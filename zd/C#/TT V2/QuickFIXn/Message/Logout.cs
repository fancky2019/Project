// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class Logout : Message
        {
            public const string MsgType = "5";

            public Logout() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("5"));
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
            public QuickFix.Fields.ForceLogout ForceLogout
            { 
                get 
                {
                    QuickFix.Fields.ForceLogout val = new QuickFix.Fields.ForceLogout();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ForceLogout val) 
            { 
                this.ForceLogout = val;
            }
            
            public QuickFix.Fields.ForceLogout Get(QuickFix.Fields.ForceLogout val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ForceLogout val) 
            { 
                return IsSetForceLogout();
            }
            
            public bool IsSetForceLogout() 
            { 
                return IsSetField(Tags.ForceLogout);
            }

        }
    }
}
