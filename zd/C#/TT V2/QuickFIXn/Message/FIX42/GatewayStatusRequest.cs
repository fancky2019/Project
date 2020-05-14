// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class GatewayStatusRequest : Message
        {
            public const string MsgType = "UAR";

            public GatewayStatusRequest() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("UAR"));
            }

            public GatewayStatusRequest(
                    QuickFix.Fields.GatewayStatusReqId aGatewayStatusReqId,
                    QuickFix.Fields.SubscriptionRequestType aSubscriptionRequestType
                ) : this()
            {
                this.GatewayStatusReqId = aGatewayStatusReqId;
                this.SubscriptionRequestType = aSubscriptionRequestType;
            }

            public QuickFix.Fields.GatewayStatusReqId GatewayStatusReqId
            { 
                get 
                {
                    QuickFix.Fields.GatewayStatusReqId val = new QuickFix.Fields.GatewayStatusReqId();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.GatewayStatusReqId val) 
            { 
                this.GatewayStatusReqId = val;
            }
            
            public QuickFix.Fields.GatewayStatusReqId Get(QuickFix.Fields.GatewayStatusReqId val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.GatewayStatusReqId val) 
            { 
                return IsSetGatewayStatusReqId();
            }
            
            public bool IsSetGatewayStatusReqId() 
            { 
                return IsSetField(Tags.GatewayStatusReqId);
            }
            public QuickFix.Fields.SubscriptionRequestType SubscriptionRequestType
            { 
                get 
                {
                    QuickFix.Fields.SubscriptionRequestType val = new QuickFix.Fields.SubscriptionRequestType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.SubscriptionRequestType val) 
            { 
                this.SubscriptionRequestType = val;
            }
            
            public QuickFix.Fields.SubscriptionRequestType Get(QuickFix.Fields.SubscriptionRequestType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.SubscriptionRequestType val) 
            { 
                return IsSetSubscriptionRequestType();
            }
            
            public bool IsSetSubscriptionRequestType() 
            { 
                return IsSetField(Tags.SubscriptionRequestType);
            }

        }
    }
}
