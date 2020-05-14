// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class OrderStatusRequest : Message
        {
            public const string MsgType = "H";

            public OrderStatusRequest() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("H"));
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
            public QuickFix.Fields.ClOrdID ClOrdID
            { 
                get 
                {
                    QuickFix.Fields.ClOrdID val = new QuickFix.Fields.ClOrdID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ClOrdID val) 
            { 
                this.ClOrdID = val;
            }
            
            public QuickFix.Fields.ClOrdID Get(QuickFix.Fields.ClOrdID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ClOrdID val) 
            { 
                return IsSetClOrdID();
            }
            
            public bool IsSetClOrdID() 
            { 
                return IsSetField(Tags.ClOrdID);
            }
            public QuickFix.Fields.OrderID OrderID
            { 
                get 
                {
                    QuickFix.Fields.OrderID val = new QuickFix.Fields.OrderID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OrderID val) 
            { 
                this.OrderID = val;
            }
            
            public QuickFix.Fields.OrderID Get(QuickFix.Fields.OrderID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OrderID val) 
            { 
                return IsSetOrderID();
            }
            
            public bool IsSetOrderID() 
            { 
                return IsSetField(Tags.OrderID);
            }
            public QuickFix.Fields.CompanyID CompanyID
            { 
                get 
                {
                    QuickFix.Fields.CompanyID val = new QuickFix.Fields.CompanyID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.CompanyID val) 
            { 
                this.CompanyID = val;
            }
            
            public QuickFix.Fields.CompanyID Get(QuickFix.Fields.CompanyID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.CompanyID val) 
            { 
                return IsSetCompanyID();
            }
            
            public bool IsSetCompanyID() 
            { 
                return IsSetField(Tags.CompanyID);
            }
            public QuickFix.Fields.ClearingAccountOverride ClearingAccountOverride
            { 
                get 
                {
                    QuickFix.Fields.ClearingAccountOverride val = new QuickFix.Fields.ClearingAccountOverride();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ClearingAccountOverride val) 
            { 
                this.ClearingAccountOverride = val;
            }
            
            public QuickFix.Fields.ClearingAccountOverride Get(QuickFix.Fields.ClearingAccountOverride val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ClearingAccountOverride val) 
            { 
                return IsSetClearingAccountOverride();
            }
            
            public bool IsSetClearingAccountOverride() 
            { 
                return IsSetField(Tags.ClearingAccountOverride);
            }

        }
    }
}
