// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX50 
    {
        public class PartyEntitlementsReport : Message
        {
            public const string MsgType = "CV";

            public PartyEntitlementsReport() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("CV"));
            }

            public PartyEntitlementsReport(
                    QuickFix.Fields.EntitlementsReportID aEntitlementsReportID
                ) : this()
            {
                this.EntitlementsReportID = aEntitlementsReportID;
            }

            public QuickFix.Fields.EntitlementsReportID EntitlementsReportID
            { 
                get 
                {
                    QuickFix.Fields.EntitlementsReportID val = new QuickFix.Fields.EntitlementsReportID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.EntitlementsReportID val) 
            { 
                this.EntitlementsReportID = val;
            }
            
            public QuickFix.Fields.EntitlementsReportID Get(QuickFix.Fields.EntitlementsReportID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.EntitlementsReportID val) 
            { 
                return IsSetEntitlementsReportID();
            }
            
            public bool IsSetEntitlementsReportID() 
            { 
                return IsSetField(Tags.EntitlementsReportID);
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
            public QuickFix.Fields.RequestResult RequestResult
            { 
                get 
                {
                    QuickFix.Fields.RequestResult val = new QuickFix.Fields.RequestResult();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.RequestResult val) 
            { 
                this.RequestResult = val;
            }
            
            public QuickFix.Fields.RequestResult Get(QuickFix.Fields.RequestResult val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.RequestResult val) 
            { 
                return IsSetRequestResult();
            }
            
            public bool IsSetRequestResult() 
            { 
                return IsSetField(Tags.RequestResult);
            }
            public QuickFix.Fields.TotNoPartyList TotNoPartyList
            { 
                get 
                {
                    QuickFix.Fields.TotNoPartyList val = new QuickFix.Fields.TotNoPartyList();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TotNoPartyList val) 
            { 
                this.TotNoPartyList = val;
            }
            
            public QuickFix.Fields.TotNoPartyList Get(QuickFix.Fields.TotNoPartyList val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TotNoPartyList val) 
            { 
                return IsSetTotNoPartyList();
            }
            
            public bool IsSetTotNoPartyList() 
            { 
                return IsSetField(Tags.TotNoPartyList);
            }
            public QuickFix.Fields.LastFragment LastFragment
            { 
                get 
                {
                    QuickFix.Fields.LastFragment val = new QuickFix.Fields.LastFragment();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LastFragment val) 
            { 
                this.LastFragment = val;
            }
            
            public QuickFix.Fields.LastFragment Get(QuickFix.Fields.LastFragment val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LastFragment val) 
            { 
                return IsSetLastFragment();
            }
            
            public bool IsSetLastFragment() 
            { 
                return IsSetField(Tags.LastFragment);
            }
            public QuickFix.Fields.NoPartyEntitlements NoPartyEntitlements
            { 
                get 
                {
                    QuickFix.Fields.NoPartyEntitlements val = new QuickFix.Fields.NoPartyEntitlements();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.NoPartyEntitlements val) 
            { 
                this.NoPartyEntitlements = val;
            }
            
            public QuickFix.Fields.NoPartyEntitlements Get(QuickFix.Fields.NoPartyEntitlements val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.NoPartyEntitlements val) 
            { 
                return IsSetNoPartyEntitlements();
            }
            
            public bool IsSetNoPartyEntitlements() 
            { 
                return IsSetField(Tags.NoPartyEntitlements);
            }
            public class NoPartyEntitlementsGroup : Group
            {
                public static int[] fieldOrder = {Tags.NoPartyDetails, Tags.NoEntitlements, 0};
            
                public NoPartyEntitlementsGroup() 
                  :base( Tags.NoPartyEntitlements, Tags.NoPartyDetails, fieldOrder)
                {
                }
            
                public override Group Clone()
                {
                    var clone = new NoPartyEntitlementsGroup();
                    clone.CopyStateFrom(this);
                    return clone;
                }
            
                            public QuickFix.Fields.NoPartyDetails NoPartyDetails
                { 
                    get 
                    {
                        QuickFix.Fields.NoPartyDetails val = new QuickFix.Fields.NoPartyDetails();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.NoPartyDetails val) 
                { 
                    this.NoPartyDetails = val;
                }
                
                public QuickFix.Fields.NoPartyDetails Get(QuickFix.Fields.NoPartyDetails val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.NoPartyDetails val) 
                { 
                    return IsSetNoPartyDetails();
                }
                
                public bool IsSetNoPartyDetails() 
                { 
                    return IsSetField(Tags.NoPartyDetails);
                }
                public QuickFix.Fields.NoEntitlements NoEntitlements
                { 
                    get 
                    {
                        QuickFix.Fields.NoEntitlements val = new QuickFix.Fields.NoEntitlements();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.NoEntitlements val) 
                { 
                    this.NoEntitlements = val;
                }
                
                public QuickFix.Fields.NoEntitlements Get(QuickFix.Fields.NoEntitlements val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.NoEntitlements val) 
                { 
                    return IsSetNoEntitlements();
                }
                
                public bool IsSetNoEntitlements() 
                { 
                    return IsSetField(Tags.NoEntitlements);
                }
                            public class NoPartyDetailsGroup : Group
                {
                    public static int[] fieldOrder = {Tags.PartyDetailID, Tags.PartyDetailIDSource, Tags.PartyDetailRole, 0};
                
                    public NoPartyDetailsGroup() 
                      :base( Tags.NoPartyDetails, Tags.PartyDetailID, fieldOrder)
                    {
                    }
                
                    public override Group Clone()
                    {
                        var clone = new NoPartyDetailsGroup();
                        clone.CopyStateFrom(this);
                        return clone;
                    }
                
                                    public QuickFix.Fields.PartyDetailID PartyDetailID
                    { 
                        get 
                        {
                            QuickFix.Fields.PartyDetailID val = new QuickFix.Fields.PartyDetailID();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }
                    
                    public void Set(QuickFix.Fields.PartyDetailID val) 
                    { 
                        this.PartyDetailID = val;
                    }
                    
                    public QuickFix.Fields.PartyDetailID Get(QuickFix.Fields.PartyDetailID val) 
                    { 
                        GetField(val);
                        return val;
                    }
                    
                    public bool IsSet(QuickFix.Fields.PartyDetailID val) 
                    { 
                        return IsSetPartyDetailID();
                    }
                    
                    public bool IsSetPartyDetailID() 
                    { 
                        return IsSetField(Tags.PartyDetailID);
                    }
                    public QuickFix.Fields.PartyDetailIDSource PartyDetailIDSource
                    { 
                        get 
                        {
                            QuickFix.Fields.PartyDetailIDSource val = new QuickFix.Fields.PartyDetailIDSource();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }
                    
                    public void Set(QuickFix.Fields.PartyDetailIDSource val) 
                    { 
                        this.PartyDetailIDSource = val;
                    }
                    
                    public QuickFix.Fields.PartyDetailIDSource Get(QuickFix.Fields.PartyDetailIDSource val) 
                    { 
                        GetField(val);
                        return val;
                    }
                    
                    public bool IsSet(QuickFix.Fields.PartyDetailIDSource val) 
                    { 
                        return IsSetPartyDetailIDSource();
                    }
                    
                    public bool IsSetPartyDetailIDSource() 
                    { 
                        return IsSetField(Tags.PartyDetailIDSource);
                    }
                    public QuickFix.Fields.PartyDetailRole PartyDetailRole
                    { 
                        get 
                        {
                            QuickFix.Fields.PartyDetailRole val = new QuickFix.Fields.PartyDetailRole();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }
                    
                    public void Set(QuickFix.Fields.PartyDetailRole val) 
                    { 
                        this.PartyDetailRole = val;
                    }
                    
                    public QuickFix.Fields.PartyDetailRole Get(QuickFix.Fields.PartyDetailRole val) 
                    { 
                        GetField(val);
                        return val;
                    }
                    
                    public bool IsSet(QuickFix.Fields.PartyDetailRole val) 
                    { 
                        return IsSetPartyDetailRole();
                    }
                    
                    public bool IsSetPartyDetailRole() 
                    { 
                        return IsSetField(Tags.PartyDetailRole);
                    }
                
                }
                public class NoEntitlementsGroup : Group
                {
                    public static int[] fieldOrder = {Tags.EntitlementIndicator, Tags.EntitlementType, Tags.NoEntitlementAttrib, Tags.EntitlementID, Tags.NoInstrumentScopes, 0};
                
                    public NoEntitlementsGroup() 
                      :base( Tags.NoEntitlements, Tags.EntitlementIndicator, fieldOrder)
                    {
                    }
                
                    public override Group Clone()
                    {
                        var clone = new NoEntitlementsGroup();
                        clone.CopyStateFrom(this);
                        return clone;
                    }
                
                                    public QuickFix.Fields.EntitlementIndicator EntitlementIndicator
                    { 
                        get 
                        {
                            QuickFix.Fields.EntitlementIndicator val = new QuickFix.Fields.EntitlementIndicator();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }
                    
                    public void Set(QuickFix.Fields.EntitlementIndicator val) 
                    { 
                        this.EntitlementIndicator = val;
                    }
                    
                    public QuickFix.Fields.EntitlementIndicator Get(QuickFix.Fields.EntitlementIndicator val) 
                    { 
                        GetField(val);
                        return val;
                    }
                    
                    public bool IsSet(QuickFix.Fields.EntitlementIndicator val) 
                    { 
                        return IsSetEntitlementIndicator();
                    }
                    
                    public bool IsSetEntitlementIndicator() 
                    { 
                        return IsSetField(Tags.EntitlementIndicator);
                    }
                    public QuickFix.Fields.EntitlementType EntitlementType
                    { 
                        get 
                        {
                            QuickFix.Fields.EntitlementType val = new QuickFix.Fields.EntitlementType();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }
                    
                    public void Set(QuickFix.Fields.EntitlementType val) 
                    { 
                        this.EntitlementType = val;
                    }
                    
                    public QuickFix.Fields.EntitlementType Get(QuickFix.Fields.EntitlementType val) 
                    { 
                        GetField(val);
                        return val;
                    }
                    
                    public bool IsSet(QuickFix.Fields.EntitlementType val) 
                    { 
                        return IsSetEntitlementType();
                    }
                    
                    public bool IsSetEntitlementType() 
                    { 
                        return IsSetField(Tags.EntitlementType);
                    }
                    public QuickFix.Fields.NoEntitlementAttrib NoEntitlementAttrib
                    { 
                        get 
                        {
                            QuickFix.Fields.NoEntitlementAttrib val = new QuickFix.Fields.NoEntitlementAttrib();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }
                    
                    public void Set(QuickFix.Fields.NoEntitlementAttrib val) 
                    { 
                        this.NoEntitlementAttrib = val;
                    }
                    
                    public QuickFix.Fields.NoEntitlementAttrib Get(QuickFix.Fields.NoEntitlementAttrib val) 
                    { 
                        GetField(val);
                        return val;
                    }
                    
                    public bool IsSet(QuickFix.Fields.NoEntitlementAttrib val) 
                    { 
                        return IsSetNoEntitlementAttrib();
                    }
                    
                    public bool IsSetNoEntitlementAttrib() 
                    { 
                        return IsSetField(Tags.NoEntitlementAttrib);
                    }
                    public QuickFix.Fields.EntitlementID EntitlementID
                    { 
                        get 
                        {
                            QuickFix.Fields.EntitlementID val = new QuickFix.Fields.EntitlementID();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }
                    
                    public void Set(QuickFix.Fields.EntitlementID val) 
                    { 
                        this.EntitlementID = val;
                    }
                    
                    public QuickFix.Fields.EntitlementID Get(QuickFix.Fields.EntitlementID val) 
                    { 
                        GetField(val);
                        return val;
                    }
                    
                    public bool IsSet(QuickFix.Fields.EntitlementID val) 
                    { 
                        return IsSetEntitlementID();
                    }
                    
                    public bool IsSetEntitlementID() 
                    { 
                        return IsSetField(Tags.EntitlementID);
                    }
                    public QuickFix.Fields.NoInstrumentScopes NoInstrumentScopes
                    { 
                        get 
                        {
                            QuickFix.Fields.NoInstrumentScopes val = new QuickFix.Fields.NoInstrumentScopes();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }
                    
                    public void Set(QuickFix.Fields.NoInstrumentScopes val) 
                    { 
                        this.NoInstrumentScopes = val;
                    }
                    
                    public QuickFix.Fields.NoInstrumentScopes Get(QuickFix.Fields.NoInstrumentScopes val) 
                    { 
                        GetField(val);
                        return val;
                    }
                    
                    public bool IsSet(QuickFix.Fields.NoInstrumentScopes val) 
                    { 
                        return IsSetNoInstrumentScopes();
                    }
                    
                    public bool IsSetNoInstrumentScopes() 
                    { 
                        return IsSetField(Tags.NoInstrumentScopes);
                    }
                                    public class NoEntitlementAttribGroup : Group
                    {
                        public static int[] fieldOrder = {Tags.EntitlementAttribType, Tags.EntitlementAttribDataType, Tags.EntitlementAttribValue, 0};
                    
                        public NoEntitlementAttribGroup() 
                          :base( Tags.NoEntitlementAttrib, Tags.EntitlementAttribType, fieldOrder)
                        {
                        }
                    
                        public override Group Clone()
                        {
                            var clone = new NoEntitlementAttribGroup();
                            clone.CopyStateFrom(this);
                            return clone;
                        }
                    
                                            public QuickFix.Fields.EntitlementAttribType EntitlementAttribType
                        { 
                            get 
                            {
                                QuickFix.Fields.EntitlementAttribType val = new QuickFix.Fields.EntitlementAttribType();
                                GetField(val);
                                return val;
                            }
                            set { SetField(value); }
                        }
                        
                        public void Set(QuickFix.Fields.EntitlementAttribType val) 
                        { 
                            this.EntitlementAttribType = val;
                        }
                        
                        public QuickFix.Fields.EntitlementAttribType Get(QuickFix.Fields.EntitlementAttribType val) 
                        { 
                            GetField(val);
                            return val;
                        }
                        
                        public bool IsSet(QuickFix.Fields.EntitlementAttribType val) 
                        { 
                            return IsSetEntitlementAttribType();
                        }
                        
                        public bool IsSetEntitlementAttribType() 
                        { 
                            return IsSetField(Tags.EntitlementAttribType);
                        }
                        public QuickFix.Fields.EntitlementAttribDataType EntitlementAttribDataType
                        { 
                            get 
                            {
                                QuickFix.Fields.EntitlementAttribDataType val = new QuickFix.Fields.EntitlementAttribDataType();
                                GetField(val);
                                return val;
                            }
                            set { SetField(value); }
                        }
                        
                        public void Set(QuickFix.Fields.EntitlementAttribDataType val) 
                        { 
                            this.EntitlementAttribDataType = val;
                        }
                        
                        public QuickFix.Fields.EntitlementAttribDataType Get(QuickFix.Fields.EntitlementAttribDataType val) 
                        { 
                            GetField(val);
                            return val;
                        }
                        
                        public bool IsSet(QuickFix.Fields.EntitlementAttribDataType val) 
                        { 
                            return IsSetEntitlementAttribDataType();
                        }
                        
                        public bool IsSetEntitlementAttribDataType() 
                        { 
                            return IsSetField(Tags.EntitlementAttribDataType);
                        }
                        public QuickFix.Fields.EntitlementAttribValue EntitlementAttribValue
                        { 
                            get 
                            {
                                QuickFix.Fields.EntitlementAttribValue val = new QuickFix.Fields.EntitlementAttribValue();
                                GetField(val);
                                return val;
                            }
                            set { SetField(value); }
                        }
                        
                        public void Set(QuickFix.Fields.EntitlementAttribValue val) 
                        { 
                            this.EntitlementAttribValue = val;
                        }
                        
                        public QuickFix.Fields.EntitlementAttribValue Get(QuickFix.Fields.EntitlementAttribValue val) 
                        { 
                            GetField(val);
                            return val;
                        }
                        
                        public bool IsSet(QuickFix.Fields.EntitlementAttribValue val) 
                        { 
                            return IsSetEntitlementAttribValue();
                        }
                        
                        public bool IsSetEntitlementAttribValue() 
                        { 
                            return IsSetField(Tags.EntitlementAttribValue);
                        }
                    
                    }
                    public class NoInstrumentScopesGroup : Group
                    {
                        public static int[] fieldOrder = {Tags.InstrumentScopeOperator, Tags.InstrumentScopeSecurityID, Tags.InstrumentScopeSecurityIDSource, Tags.InstrumentScopeSecurityExchange, 0};
                    
                        public NoInstrumentScopesGroup() 
                          :base( Tags.NoInstrumentScopes, Tags.InstrumentScopeOperator, fieldOrder)
                        {
                        }
                    
                        public override Group Clone()
                        {
                            var clone = new NoInstrumentScopesGroup();
                            clone.CopyStateFrom(this);
                            return clone;
                        }
                    
                                            public QuickFix.Fields.InstrumentScopeOperator InstrumentScopeOperator
                        { 
                            get 
                            {
                                QuickFix.Fields.InstrumentScopeOperator val = new QuickFix.Fields.InstrumentScopeOperator();
                                GetField(val);
                                return val;
                            }
                            set { SetField(value); }
                        }
                        
                        public void Set(QuickFix.Fields.InstrumentScopeOperator val) 
                        { 
                            this.InstrumentScopeOperator = val;
                        }
                        
                        public QuickFix.Fields.InstrumentScopeOperator Get(QuickFix.Fields.InstrumentScopeOperator val) 
                        { 
                            GetField(val);
                            return val;
                        }
                        
                        public bool IsSet(QuickFix.Fields.InstrumentScopeOperator val) 
                        { 
                            return IsSetInstrumentScopeOperator();
                        }
                        
                        public bool IsSetInstrumentScopeOperator() 
                        { 
                            return IsSetField(Tags.InstrumentScopeOperator);
                        }
                        public QuickFix.Fields.InstrumentScopeSecurityID InstrumentScopeSecurityID
                        { 
                            get 
                            {
                                QuickFix.Fields.InstrumentScopeSecurityID val = new QuickFix.Fields.InstrumentScopeSecurityID();
                                GetField(val);
                                return val;
                            }
                            set { SetField(value); }
                        }
                        
                        public void Set(QuickFix.Fields.InstrumentScopeSecurityID val) 
                        { 
                            this.InstrumentScopeSecurityID = val;
                        }
                        
                        public QuickFix.Fields.InstrumentScopeSecurityID Get(QuickFix.Fields.InstrumentScopeSecurityID val) 
                        { 
                            GetField(val);
                            return val;
                        }
                        
                        public bool IsSet(QuickFix.Fields.InstrumentScopeSecurityID val) 
                        { 
                            return IsSetInstrumentScopeSecurityID();
                        }
                        
                        public bool IsSetInstrumentScopeSecurityID() 
                        { 
                            return IsSetField(Tags.InstrumentScopeSecurityID);
                        }
                        public QuickFix.Fields.InstrumentScopeSecurityIDSource InstrumentScopeSecurityIDSource
                        { 
                            get 
                            {
                                QuickFix.Fields.InstrumentScopeSecurityIDSource val = new QuickFix.Fields.InstrumentScopeSecurityIDSource();
                                GetField(val);
                                return val;
                            }
                            set { SetField(value); }
                        }
                        
                        public void Set(QuickFix.Fields.InstrumentScopeSecurityIDSource val) 
                        { 
                            this.InstrumentScopeSecurityIDSource = val;
                        }
                        
                        public QuickFix.Fields.InstrumentScopeSecurityIDSource Get(QuickFix.Fields.InstrumentScopeSecurityIDSource val) 
                        { 
                            GetField(val);
                            return val;
                        }
                        
                        public bool IsSet(QuickFix.Fields.InstrumentScopeSecurityIDSource val) 
                        { 
                            return IsSetInstrumentScopeSecurityIDSource();
                        }
                        
                        public bool IsSetInstrumentScopeSecurityIDSource() 
                        { 
                            return IsSetField(Tags.InstrumentScopeSecurityIDSource);
                        }
                        public QuickFix.Fields.InstrumentScopeSecurityExchange InstrumentScopeSecurityExchange
                        { 
                            get 
                            {
                                QuickFix.Fields.InstrumentScopeSecurityExchange val = new QuickFix.Fields.InstrumentScopeSecurityExchange();
                                GetField(val);
                                return val;
                            }
                            set { SetField(value); }
                        }
                        
                        public void Set(QuickFix.Fields.InstrumentScopeSecurityExchange val) 
                        { 
                            this.InstrumentScopeSecurityExchange = val;
                        }
                        
                        public QuickFix.Fields.InstrumentScopeSecurityExchange Get(QuickFix.Fields.InstrumentScopeSecurityExchange val) 
                        { 
                            GetField(val);
                            return val;
                        }
                        
                        public bool IsSet(QuickFix.Fields.InstrumentScopeSecurityExchange val) 
                        { 
                            return IsSetInstrumentScopeSecurityExchange();
                        }
                        
                        public bool IsSetInstrumentScopeSecurityExchange() 
                        { 
                            return IsSetField(Tags.InstrumentScopeSecurityExchange);
                        }
                    
                    }
                }
            }
        }
    }
}
