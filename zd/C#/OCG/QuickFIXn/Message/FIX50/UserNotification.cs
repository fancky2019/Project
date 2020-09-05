// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX50 
    {
        public class UserNotification : Message
        {
            public const string MsgType = "CB";

            public UserNotification() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("CB"));
            }

            public UserNotification(
                    QuickFix.Fields.UserStatus aUserStatus
                ) : this()
            {
                this.UserStatus = aUserStatus;
            }

            public QuickFix.Fields.NoUsernames NoUsernames
            { 
                get 
                {
                    QuickFix.Fields.NoUsernames val = new QuickFix.Fields.NoUsernames();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.NoUsernames val) 
            { 
                this.NoUsernames = val;
            }
            
            public QuickFix.Fields.NoUsernames Get(QuickFix.Fields.NoUsernames val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.NoUsernames val) 
            { 
                return IsSetNoUsernames();
            }
            
            public bool IsSetNoUsernames() 
            { 
                return IsSetField(Tags.NoUsernames);
            }
            public QuickFix.Fields.UserStatus UserStatus
            { 
                get 
                {
                    QuickFix.Fields.UserStatus val = new QuickFix.Fields.UserStatus();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.UserStatus val) 
            { 
                this.UserStatus = val;
            }
            
            public QuickFix.Fields.UserStatus Get(QuickFix.Fields.UserStatus val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.UserStatus val) 
            { 
                return IsSetUserStatus();
            }
            
            public bool IsSetUserStatus() 
            { 
                return IsSetField(Tags.UserStatus);
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
            public QuickFix.Fields.EncodedTextLen EncodedTextLen
            { 
                get 
                {
                    QuickFix.Fields.EncodedTextLen val = new QuickFix.Fields.EncodedTextLen();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.EncodedTextLen val) 
            { 
                this.EncodedTextLen = val;
            }
            
            public QuickFix.Fields.EncodedTextLen Get(QuickFix.Fields.EncodedTextLen val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.EncodedTextLen val) 
            { 
                return IsSetEncodedTextLen();
            }
            
            public bool IsSetEncodedTextLen() 
            { 
                return IsSetField(Tags.EncodedTextLen);
            }
            public QuickFix.Fields.EncodedText EncodedText
            { 
                get 
                {
                    QuickFix.Fields.EncodedText val = new QuickFix.Fields.EncodedText();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.EncodedText val) 
            { 
                this.EncodedText = val;
            }
            
            public QuickFix.Fields.EncodedText Get(QuickFix.Fields.EncodedText val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.EncodedText val) 
            { 
                return IsSetEncodedText();
            }
            
            public bool IsSetEncodedText() 
            { 
                return IsSetField(Tags.EncodedText);
            }
            public class NoUsernamesGroup : Group
            {
                public static int[] fieldOrder = {Tags.Username, 0};
            
                public NoUsernamesGroup() 
                  :base( Tags.NoUsernames, Tags.Username, fieldOrder)
                {
                }
            
                public override Group Clone()
                {
                    var clone = new NoUsernamesGroup();
                    clone.CopyStateFrom(this);
                    return clone;
                }
            
                            public QuickFix.Fields.Username Username
                { 
                    get 
                    {
                        QuickFix.Fields.Username val = new QuickFix.Fields.Username();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.Username val) 
                { 
                    this.Username = val;
                }
                
                public QuickFix.Fields.Username Get(QuickFix.Fields.Username val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.Username val) 
                { 
                    return IsSetUsername();
                }
                
                public bool IsSetUsername() 
                { 
                    return IsSetField(Tags.Username);
                }
            
            }
        }
    }
}
