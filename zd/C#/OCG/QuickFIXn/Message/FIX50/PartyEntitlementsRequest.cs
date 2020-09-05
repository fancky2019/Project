// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX50 
    {
        public class PartyEntitlementsRequest : Message
        {
            public const string MsgType = "CU";

            public PartyEntitlementsRequest() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("CU"));
            }

            public PartyEntitlementsRequest(
                    QuickFix.Fields.EntitlementsRequestID aEntitlementsRequestID
                ) : this()
            {
                this.EntitlementsRequestID = aEntitlementsRequestID;
            }

            public QuickFix.Fields.EntitlementsRequestID EntitlementsRequestID
            { 
                get 
                {
                    QuickFix.Fields.EntitlementsRequestID val = new QuickFix.Fields.EntitlementsRequestID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.EntitlementsRequestID val) 
            { 
                this.EntitlementsRequestID = val;
            }
            
            public QuickFix.Fields.EntitlementsRequestID Get(QuickFix.Fields.EntitlementsRequestID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.EntitlementsRequestID val) 
            { 
                return IsSetEntitlementsRequestID();
            }
            
            public bool IsSetEntitlementsRequestID() 
            { 
                return IsSetField(Tags.EntitlementsRequestID);
            }

        }
    }
}
