using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QuickFix.Fields;
using System.Reflection;
using System.Linq.Expressions;

namespace QuickFix
{
    /// <summary>
    /// Helper class for delegating message types for various FIX versions to
    /// type-safe OnMessage methods.
    /// </summary>
    public abstract class MessageCracker
    {
        /// <summary>
        /// 根据MarketApp类内OnMessage(QuickFix.FIX42.SecurityDefinition msg, QuickFix.SessionID s)
        /// 的重载，OnMessage的第一个参数的类型注册回调。
        /// </summary>
        private Dictionary<Type, Action<Message, SessionID>> _callCache = new Dictionary<Type, Action<Message, SessionID>>();

        public MessageCracker()
        {
            Initialize(this);
        }

        private void Initialize(Object messageHandler)
        {
            Type handlerType = messageHandler.GetType();

            MethodInfo[] methods = handlerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (MethodInfo m in methods)
            {
                 TryBuildCallCache(m);
            }
        }

        /// <summary>
        /// build  a complied expression tree - much faster than calling MethodInfo.Invoke
        /// </summary>
        /// <param name="m"></param>
        private void TryBuildCallCache(MethodInfo m)
        {
            if (IsHandlerMethod(m))
            {
                var parameters = m.GetParameters();

                var expParamMessage = parameters[0];

                var expParamSessionId = parameters[1];

                var messageParam = Expression.Parameter(typeof(Message), "message");

                var sessionParam = Expression.Parameter(typeof(SessionID), "sessionID");

                var instance = Expression.Constant(this);

                var methodCall = Expression.Call(instance, m, Expression.Convert(messageParam, expParamMessage.ParameterType), Expression.Convert(sessionParam, expParamSessionId.ParameterType));

                var action = Expression.Lambda<Action<Message, SessionID>>(methodCall, messageParam, sessionParam).Compile();

                _callCache[expParamMessage.ParameterType] = action;

            }
        }


        static public bool IsHandlerMethod(MethodInfo m)
        {
            return (m.IsPublic == true
                && m.Name.Equals("OnMessage")
                && m.GetParameters().Length == 2
                && m.GetParameters()[0].ParameterType.IsSubclassOf(typeof(QuickFix.Message))
                && typeof(QuickFix.SessionID).IsAssignableFrom(m.GetParameters()[1].ParameterType)
                && m.ReturnType == typeof(void));
        }


        /// <summary>
        /// Process ("crack") a FIX message and call the registered handlers for that type, if any
        /// 
        /// 
        /// 1、QuickFix里的Session类的方法 Next(Message message)接收TT返回的消息（while read()返回的，往上在跟踪），方法里调用this.Application.FromApp(msg, this.SessionID);
        /// 2、FromApp(QuickFix.Message message, QuickFix.SessionID sessionID)调用Crack(Message message, SessionID sessionID)
        /// 3、 Crack(Message message, SessionID sessionID)根据反射，调用 onMessage(message, sessionID);方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sessionID"></param>
        public void Crack(Message message, SessionID sessionID)
        {
            Type messageType = message.GetType();
            Action<Message, SessionID> onMessage = null;
            if (_callCache.TryGetValue(messageType, out onMessage))
            {
                onMessage(message, sessionID);
            }
            else
            {
                throw new UnsupportedMessageType();
            }
        }
    }
}
