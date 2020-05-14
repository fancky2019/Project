// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class RequestForPosition : Message
        {
            public const string MsgType = "UAN";

            public RequestForPosition() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("UAN"));
            }

            public RequestForPosition(
                    QuickFix.Fields.PosReqId aPosReqId,
                    QuickFix.Fields.PosReqType aPosReqType
                ) : this()
            {
                this.PosReqId = aPosReqId;
                this.PosReqType = aPosReqType;
            }

            public QuickFix.Fields.Account Account
            { 
                get 
                {
                    QuickFix.Fields.Account val = new QuickFix.Fields.Account();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.Account val) 
            { 
                this.Account = val;
            }
            
            public QuickFix.Fields.Account Get(QuickFix.Fields.Account val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.Account val) 
            { 
                return IsSetAccount();
            }
            
            public bool IsSetAccount() 
            { 
                return IsSetField(Tags.Account);
            }
            public QuickFix.Fields.PosReqId PosReqId
            { 
                get 
                {
                    QuickFix.Fields.PosReqId val = new QuickFix.Fields.PosReqId();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.PosReqId val) 
            { 
                this.PosReqId = val;
            }
            
            public QuickFix.Fields.PosReqId Get(QuickFix.Fields.PosReqId val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.PosReqId val) 
            { 
                return IsSetPosReqId();
            }
            
            public bool IsSetPosReqId() 
            { 
                return IsSetField(Tags.PosReqId);
            }
            public QuickFix.Fields.PosReqType PosReqType
            { 
                get 
                {
                    QuickFix.Fields.PosReqType val = new QuickFix.Fields.PosReqType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.PosReqType val) 
            { 
                this.PosReqType = val;
            }
            
            public QuickFix.Fields.PosReqType Get(QuickFix.Fields.PosReqType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.PosReqType val) 
            { 
                return IsSetPosReqType();
            }
            
            public bool IsSetPosReqType() 
            { 
                return IsSetField(Tags.PosReqType);
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
            public QuickFix.Fields.IncludeHistoricalFills IncludeHistoricalFills
            { 
                get 
                {
                    QuickFix.Fields.IncludeHistoricalFills val = new QuickFix.Fields.IncludeHistoricalFills();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.IncludeHistoricalFills val) 
            { 
                this.IncludeHistoricalFills = val;
            }
            
            public QuickFix.Fields.IncludeHistoricalFills Get(QuickFix.Fields.IncludeHistoricalFills val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.IncludeHistoricalFills val) 
            { 
                return IsSetIncludeHistoricalFills();
            }
            
            public bool IsSetIncludeHistoricalFills() 
            { 
                return IsSetField(Tags.IncludeHistoricalFills);
            }

        }
    }
}
