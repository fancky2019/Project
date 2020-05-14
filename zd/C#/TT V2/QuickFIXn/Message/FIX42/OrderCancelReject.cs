// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class OrderCancelReject : Message
        {
            public const string MsgType = "9";

            public OrderCancelReject() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("9"));
            }

            public OrderCancelReject(
                    QuickFix.Fields.OrderID aOrderID,
                    QuickFix.Fields.ClOrdID aClOrdID,
                    QuickFix.Fields.OrdStatus aOrdStatus,
                    QuickFix.Fields.CxlRejResponseTo aCxlRejResponseTo
                ) : this()
            {
                this.OrderID = aOrderID;
                this.ClOrdID = aClOrdID;
                this.OrdStatus = aOrdStatus;
                this.CxlRejResponseTo = aCxlRejResponseTo;
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
            public QuickFix.Fields.SecondaryOrderID SecondaryOrderID
            { 
                get 
                {
                    QuickFix.Fields.SecondaryOrderID val = new QuickFix.Fields.SecondaryOrderID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.SecondaryOrderID val) 
            { 
                this.SecondaryOrderID = val;
            }
            
            public QuickFix.Fields.SecondaryOrderID Get(QuickFix.Fields.SecondaryOrderID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.SecondaryOrderID val) 
            { 
                return IsSetSecondaryOrderID();
            }
            
            public bool IsSetSecondaryOrderID() 
            { 
                return IsSetField(Tags.SecondaryOrderID);
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
            public QuickFix.Fields.TTClOrdID TTClOrdID
            { 
                get 
                {
                    QuickFix.Fields.TTClOrdID val = new QuickFix.Fields.TTClOrdID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTClOrdID val) 
            { 
                this.TTClOrdID = val;
            }
            
            public QuickFix.Fields.TTClOrdID Get(QuickFix.Fields.TTClOrdID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTClOrdID val) 
            { 
                return IsSetTTClOrdID();
            }
            
            public bool IsSetTTClOrdID() 
            { 
                return IsSetField(Tags.TTClOrdID);
            }
            public QuickFix.Fields.OrigClOrdID OrigClOrdID
            { 
                get 
                {
                    QuickFix.Fields.OrigClOrdID val = new QuickFix.Fields.OrigClOrdID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OrigClOrdID val) 
            { 
                this.OrigClOrdID = val;
            }
            
            public QuickFix.Fields.OrigClOrdID Get(QuickFix.Fields.OrigClOrdID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OrigClOrdID val) 
            { 
                return IsSetOrigClOrdID();
            }
            
            public bool IsSetOrigClOrdID() 
            { 
                return IsSetField(Tags.OrigClOrdID);
            }
            public QuickFix.Fields.OrdStatus OrdStatus
            { 
                get 
                {
                    QuickFix.Fields.OrdStatus val = new QuickFix.Fields.OrdStatus();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OrdStatus val) 
            { 
                this.OrdStatus = val;
            }
            
            public QuickFix.Fields.OrdStatus Get(QuickFix.Fields.OrdStatus val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OrdStatus val) 
            { 
                return IsSetOrdStatus();
            }
            
            public bool IsSetOrdStatus() 
            { 
                return IsSetField(Tags.OrdStatus);
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
            public QuickFix.Fields.TransactTime TransactTime
            { 
                get 
                {
                    QuickFix.Fields.TransactTime val = new QuickFix.Fields.TransactTime();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TransactTime val) 
            { 
                this.TransactTime = val;
            }
            
            public QuickFix.Fields.TransactTime Get(QuickFix.Fields.TransactTime val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TransactTime val) 
            { 
                return IsSetTransactTime();
            }
            
            public bool IsSetTransactTime() 
            { 
                return IsSetField(Tags.TransactTime);
            }
            public QuickFix.Fields.CxlRejResponseTo CxlRejResponseTo
            { 
                get 
                {
                    QuickFix.Fields.CxlRejResponseTo val = new QuickFix.Fields.CxlRejResponseTo();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.CxlRejResponseTo val) 
            { 
                this.CxlRejResponseTo = val;
            }
            
            public QuickFix.Fields.CxlRejResponseTo Get(QuickFix.Fields.CxlRejResponseTo val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.CxlRejResponseTo val) 
            { 
                return IsSetCxlRejResponseTo();
            }
            
            public bool IsSetCxlRejResponseTo() 
            { 
                return IsSetField(Tags.CxlRejResponseTo);
            }
            public QuickFix.Fields.CxlRejReason CxlRejReason
            { 
                get 
                {
                    QuickFix.Fields.CxlRejReason val = new QuickFix.Fields.CxlRejReason();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.CxlRejReason val) 
            { 
                this.CxlRejReason = val;
            }
            
            public QuickFix.Fields.CxlRejReason Get(QuickFix.Fields.CxlRejReason val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.CxlRejReason val) 
            { 
                return IsSetCxlRejReason();
            }
            
            public bool IsSetCxlRejReason() 
            { 
                return IsSetField(Tags.CxlRejReason);
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
            public QuickFix.Fields.TTID TTID
            { 
                get 
                {
                    QuickFix.Fields.TTID val = new QuickFix.Fields.TTID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTID val) 
            { 
                this.TTID = val;
            }
            
            public QuickFix.Fields.TTID Get(QuickFix.Fields.TTID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTID val) 
            { 
                return IsSetTTID();
            }
            
            public bool IsSetTTID() 
            { 
                return IsSetField(Tags.TTID);
            }
            public QuickFix.Fields.AOTCPreventionActionType AOTCPreventionActionType
            { 
                get 
                {
                    QuickFix.Fields.AOTCPreventionActionType val = new QuickFix.Fields.AOTCPreventionActionType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.AOTCPreventionActionType val) 
            { 
                this.AOTCPreventionActionType = val;
            }
            
            public QuickFix.Fields.AOTCPreventionActionType Get(QuickFix.Fields.AOTCPreventionActionType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.AOTCPreventionActionType val) 
            { 
                return IsSetAOTCPreventionActionType();
            }
            
            public bool IsSetAOTCPreventionActionType() 
            { 
                return IsSetField(Tags.AOTCPreventionActionType);
            }
            public QuickFix.Fields.BrokerID BrokerID
            { 
                get 
                {
                    QuickFix.Fields.BrokerID val = new QuickFix.Fields.BrokerID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.BrokerID val) 
            { 
                this.BrokerID = val;
            }
            
            public QuickFix.Fields.BrokerID Get(QuickFix.Fields.BrokerID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.BrokerID val) 
            { 
                return IsSetBrokerID();
            }
            
            public bool IsSetBrokerID() 
            { 
                return IsSetField(Tags.BrokerID);
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
            public QuickFix.Fields.HandlInst HandlInst
            { 
                get 
                {
                    QuickFix.Fields.HandlInst val = new QuickFix.Fields.HandlInst();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.HandlInst val) 
            { 
                this.HandlInst = val;
            }
            
            public QuickFix.Fields.HandlInst Get(QuickFix.Fields.HandlInst val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.HandlInst val) 
            { 
                return IsSetHandlInst();
            }
            
            public bool IsSetHandlInst() 
            { 
                return IsSetField(Tags.HandlInst);
            }
            public QuickFix.Fields.StagedOrderMsg StagedOrderMsg
            { 
                get 
                {
                    QuickFix.Fields.StagedOrderMsg val = new QuickFix.Fields.StagedOrderMsg();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.StagedOrderMsg val) 
            { 
                this.StagedOrderMsg = val;
            }
            
            public QuickFix.Fields.StagedOrderMsg Get(QuickFix.Fields.StagedOrderMsg val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.StagedOrderMsg val) 
            { 
                return IsSetStagedOrderMsg();
            }
            
            public bool IsSetStagedOrderMsg() 
            { 
                return IsSetField(Tags.StagedOrderMsg);
            }
            public QuickFix.Fields.StagedRoutingLevel StagedRoutingLevel
            { 
                get 
                {
                    QuickFix.Fields.StagedRoutingLevel val = new QuickFix.Fields.StagedRoutingLevel();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.StagedRoutingLevel val) 
            { 
                this.StagedRoutingLevel = val;
            }
            
            public QuickFix.Fields.StagedRoutingLevel Get(QuickFix.Fields.StagedRoutingLevel val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.StagedRoutingLevel val) 
            { 
                return IsSetStagedRoutingLevel();
            }
            
            public bool IsSetStagedRoutingLevel() 
            { 
                return IsSetField(Tags.StagedRoutingLevel);
            }
            public QuickFix.Fields.StagedOrderOwner StagedOrderOwner
            { 
                get 
                {
                    QuickFix.Fields.StagedOrderOwner val = new QuickFix.Fields.StagedOrderOwner();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.StagedOrderOwner val) 
            { 
                this.StagedOrderOwner = val;
            }
            
            public QuickFix.Fields.StagedOrderOwner Get(QuickFix.Fields.StagedOrderOwner val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.StagedOrderOwner val) 
            { 
                return IsSetStagedOrderOwner();
            }
            
            public bool IsSetStagedOrderOwner() 
            { 
                return IsSetField(Tags.StagedOrderOwner);
            }
            public QuickFix.Fields.StagedOrderStatus StagedOrderStatus
            { 
                get 
                {
                    QuickFix.Fields.StagedOrderStatus val = new QuickFix.Fields.StagedOrderStatus();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.StagedOrderStatus val) 
            { 
                this.StagedOrderStatus = val;
            }
            
            public QuickFix.Fields.StagedOrderStatus Get(QuickFix.Fields.StagedOrderStatus val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.StagedOrderStatus val) 
            { 
                return IsSetStagedOrderStatus();
            }
            
            public bool IsSetStagedOrderStatus() 
            { 
                return IsSetField(Tags.StagedOrderStatus);
            }
            public QuickFix.Fields.ExternalSource ExternalSource
            { 
                get 
                {
                    QuickFix.Fields.ExternalSource val = new QuickFix.Fields.ExternalSource();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ExternalSource val) 
            { 
                this.ExternalSource = val;
            }
            
            public QuickFix.Fields.ExternalSource Get(QuickFix.Fields.ExternalSource val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ExternalSource val) 
            { 
                return IsSetExternalSource();
            }
            
            public bool IsSetExternalSource() 
            { 
                return IsSetField(Tags.ExternalSource);
            }
            public QuickFix.Fields.OrderIDGUID OrderIDGUID
            { 
                get 
                {
                    QuickFix.Fields.OrderIDGUID val = new QuickFix.Fields.OrderIDGUID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OrderIDGUID val) 
            { 
                this.OrderIDGUID = val;
            }
            
            public QuickFix.Fields.OrderIDGUID Get(QuickFix.Fields.OrderIDGUID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OrderIDGUID val) 
            { 
                return IsSetOrderIDGUID();
            }
            
            public bool IsSetOrderIDGUID() 
            { 
                return IsSetField(Tags.OrderIDGUID);
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
            public QuickFix.Fields.TextTT TextTT
            { 
                get 
                {
                    QuickFix.Fields.TextTT val = new QuickFix.Fields.TextTT();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TextTT val) 
            { 
                this.TextTT = val;
            }
            
            public QuickFix.Fields.TextTT Get(QuickFix.Fields.TextTT val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TextTT val) 
            { 
                return IsSetTextTT();
            }
            
            public bool IsSetTextTT() 
            { 
                return IsSetField(Tags.TextTT);
            }
            public QuickFix.Fields.NoStrategyParameters NoStrategyParameters
            { 
                get 
                {
                    QuickFix.Fields.NoStrategyParameters val = new QuickFix.Fields.NoStrategyParameters();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.NoStrategyParameters val) 
            { 
                this.NoStrategyParameters = val;
            }
            
            public QuickFix.Fields.NoStrategyParameters Get(QuickFix.Fields.NoStrategyParameters val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.NoStrategyParameters val) 
            { 
                return IsSetNoStrategyParameters();
            }
            
            public bool IsSetNoStrategyParameters() 
            { 
                return IsSetField(Tags.NoStrategyParameters);
            }
            public class NoStrategyParametersGroup : Group
            {
                public static int[] fieldOrder = {Tags.StrategyParameterName, Tags.StrategyParameterType, Tags.StrategyParameterValue, 0};
            
                public NoStrategyParametersGroup() 
                  :base( Tags.NoStrategyParameters, Tags.StrategyParameterName, fieldOrder)
                {
                }
            
                public override Group Clone()
                {
                    var clone = new NoStrategyParametersGroup();
                    clone.CopyStateFrom(this);
                    return clone;
                }
            
                            public QuickFix.Fields.StrategyParameterName StrategyParameterName
                { 
                    get 
                    {
                        QuickFix.Fields.StrategyParameterName val = new QuickFix.Fields.StrategyParameterName();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.StrategyParameterName val) 
                { 
                    this.StrategyParameterName = val;
                }
                
                public QuickFix.Fields.StrategyParameterName Get(QuickFix.Fields.StrategyParameterName val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.StrategyParameterName val) 
                { 
                    return IsSetStrategyParameterName();
                }
                
                public bool IsSetStrategyParameterName() 
                { 
                    return IsSetField(Tags.StrategyParameterName);
                }
                public QuickFix.Fields.StrategyParameterType StrategyParameterType
                { 
                    get 
                    {
                        QuickFix.Fields.StrategyParameterType val = new QuickFix.Fields.StrategyParameterType();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.StrategyParameterType val) 
                { 
                    this.StrategyParameterType = val;
                }
                
                public QuickFix.Fields.StrategyParameterType Get(QuickFix.Fields.StrategyParameterType val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.StrategyParameterType val) 
                { 
                    return IsSetStrategyParameterType();
                }
                
                public bool IsSetStrategyParameterType() 
                { 
                    return IsSetField(Tags.StrategyParameterType);
                }
                public QuickFix.Fields.StrategyParameterValue StrategyParameterValue
                { 
                    get 
                    {
                        QuickFix.Fields.StrategyParameterValue val = new QuickFix.Fields.StrategyParameterValue();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.StrategyParameterValue val) 
                { 
                    this.StrategyParameterValue = val;
                }
                
                public QuickFix.Fields.StrategyParameterValue Get(QuickFix.Fields.StrategyParameterValue val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.StrategyParameterValue val) 
                { 
                    return IsSetStrategyParameterValue();
                }
                
                public bool IsSetStrategyParameterValue() 
                { 
                    return IsSetField(Tags.StrategyParameterValue);
                }
            
            }
        }
    }
}
