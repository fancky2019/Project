// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX50SP2 
    {
        public class UserResponse : Message
        {
            public const string MsgType = "BF";

            public UserResponse() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("BF"));
            }

            public UserResponse(
                    QuickFix.Fields.UserRequestID aUserRequestID,
                    QuickFix.Fields.Username aUsername
                ) : this()
            {
                this.UserRequestID = aUserRequestID;
                this.Username = aUsername;
            }

            public QuickFix.Fields.UserRequestID UserRequestID
            { 
                get 
                {
                    QuickFix.Fields.UserRequestID val = new QuickFix.Fields.UserRequestID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.UserRequestID val) 
            { 
                this.UserRequestID = val;
            }
            
            public QuickFix.Fields.UserRequestID Get(QuickFix.Fields.UserRequestID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.UserRequestID val) 
            { 
                return IsSetUserRequestID();
            }
            
            public bool IsSetUserRequestID() 
            { 
                return IsSetField(Tags.UserRequestID);
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
            public QuickFix.Fields.UserStatusText UserStatusText
            { 
                get 
                {
                    QuickFix.Fields.UserStatusText val = new QuickFix.Fields.UserStatusText();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.UserStatusText val) 
            { 
                this.UserStatusText = val;
            }
            
            public QuickFix.Fields.UserStatusText Get(QuickFix.Fields.UserStatusText val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.UserStatusText val) 
            { 
                return IsSetUserStatusText();
            }
            
            public bool IsSetUserStatusText() 
            { 
                return IsSetField(Tags.UserStatusText);
            }
            public QuickFix.Fields.NoThrottles NoThrottles
            { 
                get 
                {
                    QuickFix.Fields.NoThrottles val = new QuickFix.Fields.NoThrottles();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.NoThrottles val) 
            { 
                this.NoThrottles = val;
            }
            
            public QuickFix.Fields.NoThrottles Get(QuickFix.Fields.NoThrottles val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.NoThrottles val) 
            { 
                return IsSetNoThrottles();
            }
            
            public bool IsSetNoThrottles() 
            { 
                return IsSetField(Tags.NoThrottles);
            }
            public class NoThrottlesGroup : Group
            {
                public static int[] fieldOrder = {Tags.ThrottleAction, Tags.ThrottleType, Tags.ThrottleNoMsgs, Tags.ThrottleTimeInterval, Tags.ThrottleTimeUnit, 0};
            
                public NoThrottlesGroup() 
                  :base( Tags.NoThrottles, Tags.ThrottleAction, fieldOrder)
                {
                }
            
                public override Group Clone()
                {
                    var clone = new NoThrottlesGroup();
                    clone.CopyStateFrom(this);
                    return clone;
                }
            
                public QuickFix.Fields.ThrottleAction ThrottleAction
                { 
                    get 
                    {
                        QuickFix.Fields.ThrottleAction val = new QuickFix.Fields.ThrottleAction();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.ThrottleAction val) 
                { 
                    this.ThrottleAction = val;
                }
                
                public QuickFix.Fields.ThrottleAction Get(QuickFix.Fields.ThrottleAction val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.ThrottleAction val) 
                { 
                    return IsSetThrottleAction();
                }
                
                public bool IsSetThrottleAction() 
                { 
                    return IsSetField(Tags.ThrottleAction);
                }
                public QuickFix.Fields.ThrottleType ThrottleType
                { 
                    get 
                    {
                        QuickFix.Fields.ThrottleType val = new QuickFix.Fields.ThrottleType();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.ThrottleType val) 
                { 
                    this.ThrottleType = val;
                }
                
                public QuickFix.Fields.ThrottleType Get(QuickFix.Fields.ThrottleType val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.ThrottleType val) 
                { 
                    return IsSetThrottleType();
                }
                
                public bool IsSetThrottleType() 
                { 
                    return IsSetField(Tags.ThrottleType);
                }
                public QuickFix.Fields.ThrottleNoMsgs ThrottleNoMsgs
                { 
                    get 
                    {
                        QuickFix.Fields.ThrottleNoMsgs val = new QuickFix.Fields.ThrottleNoMsgs();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.ThrottleNoMsgs val) 
                { 
                    this.ThrottleNoMsgs = val;
                }
                
                public QuickFix.Fields.ThrottleNoMsgs Get(QuickFix.Fields.ThrottleNoMsgs val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.ThrottleNoMsgs val) 
                { 
                    return IsSetThrottleNoMsgs();
                }
                
                public bool IsSetThrottleNoMsgs() 
                { 
                    return IsSetField(Tags.ThrottleNoMsgs);
                }
                public QuickFix.Fields.ThrottleTimeInterval ThrottleTimeInterval
                { 
                    get 
                    {
                        QuickFix.Fields.ThrottleTimeInterval val = new QuickFix.Fields.ThrottleTimeInterval();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.ThrottleTimeInterval val) 
                { 
                    this.ThrottleTimeInterval = val;
                }
                
                public QuickFix.Fields.ThrottleTimeInterval Get(QuickFix.Fields.ThrottleTimeInterval val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.ThrottleTimeInterval val) 
                { 
                    return IsSetThrottleTimeInterval();
                }
                
                public bool IsSetThrottleTimeInterval() 
                { 
                    return IsSetField(Tags.ThrottleTimeInterval);
                }
                public QuickFix.Fields.ThrottleTimeUnit ThrottleTimeUnit
                { 
                    get 
                    {
                        QuickFix.Fields.ThrottleTimeUnit val = new QuickFix.Fields.ThrottleTimeUnit();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.ThrottleTimeUnit val) 
                { 
                    this.ThrottleTimeUnit = val;
                }
                
                public QuickFix.Fields.ThrottleTimeUnit Get(QuickFix.Fields.ThrottleTimeUnit val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.ThrottleTimeUnit val) 
                { 
                    return IsSetThrottleTimeUnit();
                }
                
                public bool IsSetThrottleTimeUnit() 
                { 
                    return IsSetField(Tags.ThrottleTimeUnit);
                }
            
            }
        }
    }
}
