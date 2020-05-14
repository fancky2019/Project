// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class GatewayStatus : Message
        {
            public const string MsgType = "UAT";

            public GatewayStatus() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("UAT"));
            }

            public GatewayStatus(
                    QuickFix.Fields.GatewayStatusReqId aGatewayStatusReqId
                ) : this()
            {
                this.GatewayStatusReqId = aGatewayStatusReqId;
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
            public QuickFix.Fields.UnsolicitedIndicator UnsolicitedIndicator
            { 
                get 
                {
                    QuickFix.Fields.UnsolicitedIndicator val = new QuickFix.Fields.UnsolicitedIndicator();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.UnsolicitedIndicator val) 
            { 
                this.UnsolicitedIndicator = val;
            }
            
            public QuickFix.Fields.UnsolicitedIndicator Get(QuickFix.Fields.UnsolicitedIndicator val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.UnsolicitedIndicator val) 
            { 
                return IsSetUnsolicitedIndicator();
            }
            
            public bool IsSetUnsolicitedIndicator() 
            { 
                return IsSetField(Tags.UnsolicitedIndicator);
            }
            public QuickFix.Fields.NoGatewayStatus NoGatewayStatus
            { 
                get 
                {
                    QuickFix.Fields.NoGatewayStatus val = new QuickFix.Fields.NoGatewayStatus();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.NoGatewayStatus val) 
            { 
                this.NoGatewayStatus = val;
            }
            
            public QuickFix.Fields.NoGatewayStatus Get(QuickFix.Fields.NoGatewayStatus val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.NoGatewayStatus val) 
            { 
                return IsSetNoGatewayStatus();
            }
            
            public bool IsSetNoGatewayStatus() 
            { 
                return IsSetField(Tags.NoGatewayStatus);
            }
            public class NoGatewayStatusGroup : Group
            {
                public NoGatewayStatusGroup() 
                  :base( Tags.NoGatewayStatus, Tags.GatewayStatus, fieldOrder)
                {
                }
                public static int[] fieldOrder = {Tags.GatewayStatus, Tags.SecurityExchange, Tags.ExchangeGateway, Tags.SubExchangeGateway, Tags.Text, 0};
                            public QuickFix.Fields.GatewayStatus GatewayStatus
                { 
                    get 
                    {
                        QuickFix.Fields.GatewayStatus val = new QuickFix.Fields.GatewayStatus();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.GatewayStatus val) 
                { 
                    this.GatewayStatus = val;
                }
                
                public QuickFix.Fields.GatewayStatus Get(QuickFix.Fields.GatewayStatus val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.GatewayStatus val) 
                { 
                    return IsSetGatewayStatus();
                }
                
                public bool IsSetGatewayStatus() 
                { 
                    return IsSetField(Tags.GatewayStatus);
                }
                public QuickFix.Fields.SecurityExchange SecurityExchange
                { 
                    get 
                    {
                        QuickFix.Fields.SecurityExchange val = new QuickFix.Fields.SecurityExchange();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.SecurityExchange val) 
                { 
                    this.SecurityExchange = val;
                }
                
                public QuickFix.Fields.SecurityExchange Get(QuickFix.Fields.SecurityExchange val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.SecurityExchange val) 
                { 
                    return IsSetSecurityExchange();
                }
                
                public bool IsSetSecurityExchange() 
                { 
                    return IsSetField(Tags.SecurityExchange);
                }
                public QuickFix.Fields.ExchangeGateway ExchangeGateway
                { 
                    get 
                    {
                        QuickFix.Fields.ExchangeGateway val = new QuickFix.Fields.ExchangeGateway();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.ExchangeGateway val) 
                { 
                    this.ExchangeGateway = val;
                }
                
                public QuickFix.Fields.ExchangeGateway Get(QuickFix.Fields.ExchangeGateway val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.ExchangeGateway val) 
                { 
                    return IsSetExchangeGateway();
                }
                
                public bool IsSetExchangeGateway() 
                { 
                    return IsSetField(Tags.ExchangeGateway);
                }
                public QuickFix.Fields.SubExchangeGateway SubExchangeGateway
                { 
                    get 
                    {
                        QuickFix.Fields.SubExchangeGateway val = new QuickFix.Fields.SubExchangeGateway();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.SubExchangeGateway val) 
                { 
                    this.SubExchangeGateway = val;
                }
                
                public QuickFix.Fields.SubExchangeGateway Get(QuickFix.Fields.SubExchangeGateway val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.SubExchangeGateway val) 
                { 
                    return IsSetSubExchangeGateway();
                }
                
                public bool IsSetSubExchangeGateway() 
                { 
                    return IsSetField(Tags.SubExchangeGateway);
                }
                public QuickFix.Fields.Text Text
                { 
                    get 
                    {
                        QuickFix.Fields.Text val = new QuickFix.Fields.Text();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.Text val) 
                { 
                    this.Text = val;
                }
                
                public QuickFix.Fields.Text Get(QuickFix.Fields.Text val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.Text val) 
                { 
                    return IsSetText();
                }
                
                public bool IsSetText() 
                { 
                    return IsSetField(Tags.Text);
                }
            
            }
        }
    }
}
