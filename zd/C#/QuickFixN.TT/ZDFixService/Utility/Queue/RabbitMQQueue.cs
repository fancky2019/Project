using CommonClassLib;
using QuickFix;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ZDFixService.Utility.Queue
{

    /// <summary>
    /// 不支持消费失败重试。不支持Fix Message队列。
    /// 
    ///序列化造成Message子类类型丢失，只支持内存队列。
    ///如果强制实现，需要修改Fix的MessageFactory相关代码，性能不太好。
    /// </summary
    public class RabbitMQQueue<T> : IMessageQueue<T>
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();

        readonly string _exchange = "OrderExchange";
        readonly string _routingKey = "OrderRoutingKey";
        readonly string _orderQueue = "OrderQueue";
        IConnection _connection = null;
        IModel _channel = null;
        IBasicProperties _properties = null;

        public event Action<T> Dequeue;


        public RabbitMQQueue()
        {
            var tradeServiceName = Configurations.Configuration["ZDFixService:ITradeService"];
            _exchange = $"{tradeServiceName}_{_exchange}";
            _routingKey = $"{tradeServiceName}_{_routingKey}";
            _orderQueue = $"{tradeServiceName}_{_orderQueue}";

            var factory = new ConnectionFactory() { HostName = "192.168.1.105", Port = 5672, UserName = "fancky", Password = "123456" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: _orderQueue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _channel.QueueBind(queue: _orderQueue,
                              exchange: _exchange,
                              routingKey: _routingKey);

            _channel.BasicQos(0, 1, false);
            Consumer();

            //生产
            _properties = _channel.CreateBasicProperties();
            _properties.Persistent = true;
            _channel.ConfirmSelect();
            _channel.BasicReturn += (object sender, global::RabbitMQ.Client.Events.BasicReturnEventArgs e) =>
            {
                var messageReturn = Encoding.UTF8.GetString(e.Body.ToArray());
            };


        }




        public void Enqueue(T t)
        {

            try
            {
                var body = MessagePackUtility.Serialize<T>(t);
                _channel.BasicPublish(exchange: _exchange,
                                     routingKey: _routingKey,
                                     mandatory: true,
                                     basicProperties: _properties,
                                     body: body);
                _channel.WaitForConfirmsOrDie(new TimeSpan(0, 0, 5));
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }

        }


        public void Consumer()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var netInfo = MessagePackUtility.Deserialize<T>(ea.Body.ToArray());
                try
                {
                    Dequeue?.Invoke(netInfo);
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _nLog.Error(ex.ToString());
                }
            };
            _channel.BasicConsume(queue: _orderQueue, autoAck: false, consumer: consumer);


        }

        public void Remove(T t)
        {
            throw new NotImplementedException();
        }

        public void WaitForCompleting()
        {
            try
            {
                if (_channel == null || _channel.IsClosed)
                {
                    return;
                }
                //判断队列是否存在,队列不存在报异常。
                QueueDeclareOk queueDeclareOk = _channel.QueueDeclarePassive(_orderQueue);
                while (queueDeclareOk.MessageCount != 0)
                {
                    //直到所有的单据处理完成。
                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                _nLog.Error(ex.ToString());
            }
            Close();
        }

        public void Close()
        {
            _channel?.Close();
            _connection?.Close();

        }
    }
}
