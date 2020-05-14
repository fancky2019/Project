// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class MarketDataSnapshotFullRefresh : Message
        {
            public const string MsgType = "W";

            public MarketDataSnapshotFullRefresh() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("W"));
            }

            public MarketDataSnapshotFullRefresh(
                    QuickFix.Fields.MDReqID aMDReqID,
                    QuickFix.Fields.Symbol aSymbol,
                    QuickFix.Fields.SecurityType aSecurityType,
                    QuickFix.Fields.SecurityID aSecurityID,
                    QuickFix.Fields.SecurityExchange aSecurityExchange
                ) : this()
            {
                this.MDReqID = aMDReqID;
                this.Symbol = aSymbol;
                this.SecurityType = aSecurityType;
                this.SecurityID = aSecurityID;
                this.SecurityExchange = aSecurityExchange;
            }

            public QuickFix.Fields.MDReqID MDReqID
            { 
                get 
                {
                    QuickFix.Fields.MDReqID val = new QuickFix.Fields.MDReqID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.MDReqID val) 
            { 
                this.MDReqID = val;
            }
            
            public QuickFix.Fields.MDReqID Get(QuickFix.Fields.MDReqID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.MDReqID val) 
            { 
                return IsSetMDReqID();
            }
            
            public bool IsSetMDReqID() 
            { 
                return IsSetField(Tags.MDReqID);
            }
            public QuickFix.Fields.Symbol Symbol
            { 
                get 
                {
                    QuickFix.Fields.Symbol val = new QuickFix.Fields.Symbol();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.Symbol val) 
            { 
                this.Symbol = val;
            }
            
            public QuickFix.Fields.Symbol Get(QuickFix.Fields.Symbol val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.Symbol val) 
            { 
                return IsSetSymbol();
            }
            
            public bool IsSetSymbol() 
            { 
                return IsSetField(Tags.Symbol);
            }
            public QuickFix.Fields.SecurityType SecurityType
            { 
                get 
                {
                    QuickFix.Fields.SecurityType val = new QuickFix.Fields.SecurityType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.SecurityType val) 
            { 
                this.SecurityType = val;
            }
            
            public QuickFix.Fields.SecurityType Get(QuickFix.Fields.SecurityType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.SecurityType val) 
            { 
                return IsSetSecurityType();
            }
            
            public bool IsSetSecurityType() 
            { 
                return IsSetField(Tags.SecurityType);
            }
            public QuickFix.Fields.MaturityMonthYear MaturityMonthYear
            { 
                get 
                {
                    QuickFix.Fields.MaturityMonthYear val = new QuickFix.Fields.MaturityMonthYear();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.MaturityMonthYear val) 
            { 
                this.MaturityMonthYear = val;
            }
            
            public QuickFix.Fields.MaturityMonthYear Get(QuickFix.Fields.MaturityMonthYear val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.MaturityMonthYear val) 
            { 
                return IsSetMaturityMonthYear();
            }
            
            public bool IsSetMaturityMonthYear() 
            { 
                return IsSetField(Tags.MaturityMonthYear);
            }
            public QuickFix.Fields.MaturityDay MaturityDay
            { 
                get 
                {
                    QuickFix.Fields.MaturityDay val = new QuickFix.Fields.MaturityDay();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.MaturityDay val) 
            { 
                this.MaturityDay = val;
            }
            
            public QuickFix.Fields.MaturityDay Get(QuickFix.Fields.MaturityDay val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.MaturityDay val) 
            { 
                return IsSetMaturityDay();
            }
            
            public bool IsSetMaturityDay() 
            { 
                return IsSetField(Tags.MaturityDay);
            }
            public QuickFix.Fields.PutOrCall PutOrCall
            { 
                get 
                {
                    QuickFix.Fields.PutOrCall val = new QuickFix.Fields.PutOrCall();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.PutOrCall val) 
            { 
                this.PutOrCall = val;
            }
            
            public QuickFix.Fields.PutOrCall Get(QuickFix.Fields.PutOrCall val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.PutOrCall val) 
            { 
                return IsSetPutOrCall();
            }
            
            public bool IsSetPutOrCall() 
            { 
                return IsSetField(Tags.PutOrCall);
            }
            public QuickFix.Fields.StrikePrice StrikePrice
            { 
                get 
                {
                    QuickFix.Fields.StrikePrice val = new QuickFix.Fields.StrikePrice();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.StrikePrice val) 
            { 
                this.StrikePrice = val;
            }
            
            public QuickFix.Fields.StrikePrice Get(QuickFix.Fields.StrikePrice val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.StrikePrice val) 
            { 
                return IsSetStrikePrice();
            }
            
            public bool IsSetStrikePrice() 
            { 
                return IsSetField(Tags.StrikePrice);
            }
            public QuickFix.Fields.OptAttribute OptAttribute
            { 
                get 
                {
                    QuickFix.Fields.OptAttribute val = new QuickFix.Fields.OptAttribute();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OptAttribute val) 
            { 
                this.OptAttribute = val;
            }
            
            public QuickFix.Fields.OptAttribute Get(QuickFix.Fields.OptAttribute val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OptAttribute val) 
            { 
                return IsSetOptAttribute();
            }
            
            public bool IsSetOptAttribute() 
            { 
                return IsSetField(Tags.OptAttribute);
            }
            public QuickFix.Fields.SecurityID SecurityID
            { 
                get 
                {
                    QuickFix.Fields.SecurityID val = new QuickFix.Fields.SecurityID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.SecurityID val) 
            { 
                this.SecurityID = val;
            }
            
            public QuickFix.Fields.SecurityID Get(QuickFix.Fields.SecurityID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.SecurityID val) 
            { 
                return IsSetSecurityID();
            }
            
            public bool IsSetSecurityID() 
            { 
                return IsSetField(Tags.SecurityID);
            }
            public QuickFix.Fields.SecurityAltID SecurityAltID
            { 
                get 
                {
                    QuickFix.Fields.SecurityAltID val = new QuickFix.Fields.SecurityAltID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.SecurityAltID val) 
            { 
                this.SecurityAltID = val;
            }
            
            public QuickFix.Fields.SecurityAltID Get(QuickFix.Fields.SecurityAltID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.SecurityAltID val) 
            { 
                return IsSetSecurityAltID();
            }
            
            public bool IsSetSecurityAltID() 
            { 
                return IsSetField(Tags.SecurityAltID);
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
            public QuickFix.Fields.SettlType SettlType
            { 
                get 
                {
                    QuickFix.Fields.SettlType val = new QuickFix.Fields.SettlType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.SettlType val) 
            { 
                this.SettlType = val;
            }
            
            public QuickFix.Fields.SettlType Get(QuickFix.Fields.SettlType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.SettlType val) 
            { 
                return IsSetSettlType();
            }
            
            public bool IsSetSettlType() 
            { 
                return IsSetField(Tags.SettlType);
            }
            public QuickFix.Fields.TotalVolumeTraded TotalVolumeTraded
            { 
                get 
                {
                    QuickFix.Fields.TotalVolumeTraded val = new QuickFix.Fields.TotalVolumeTraded();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TotalVolumeTraded val) 
            { 
                this.TotalVolumeTraded = val;
            }
            
            public QuickFix.Fields.TotalVolumeTraded Get(QuickFix.Fields.TotalVolumeTraded val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TotalVolumeTraded val) 
            { 
                return IsSetTotalVolumeTraded();
            }
            
            public bool IsSetTotalVolumeTraded() 
            { 
                return IsSetField(Tags.TotalVolumeTraded);
            }
            public QuickFix.Fields.Currency Currency
            { 
                get 
                {
                    QuickFix.Fields.Currency val = new QuickFix.Fields.Currency();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.Currency val) 
            { 
                this.Currency = val;
            }
            
            public QuickFix.Fields.Currency Get(QuickFix.Fields.Currency val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.Currency val) 
            { 
                return IsSetCurrency();
            }
            
            public bool IsSetCurrency() 
            { 
                return IsSetField(Tags.Currency);
            }
            public QuickFix.Fields.PriceFeedStatus PriceFeedStatus
            { 
                get 
                {
                    QuickFix.Fields.PriceFeedStatus val = new QuickFix.Fields.PriceFeedStatus();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.PriceFeedStatus val) 
            { 
                this.PriceFeedStatus = val;
            }
            
            public QuickFix.Fields.PriceFeedStatus Get(QuickFix.Fields.PriceFeedStatus val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.PriceFeedStatus val) 
            { 
                return IsSetPriceFeedStatus();
            }
            
            public bool IsSetPriceFeedStatus() 
            { 
                return IsSetField(Tags.PriceFeedStatus);
            }
            public QuickFix.Fields.DeliveryTerm DeliveryTerm
            { 
                get 
                {
                    QuickFix.Fields.DeliveryTerm val = new QuickFix.Fields.DeliveryTerm();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.DeliveryTerm val) 
            { 
                this.DeliveryTerm = val;
            }
            
            public QuickFix.Fields.DeliveryTerm Get(QuickFix.Fields.DeliveryTerm val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.DeliveryTerm val) 
            { 
                return IsSetDeliveryTerm();
            }
            
            public bool IsSetDeliveryTerm() 
            { 
                return IsSetField(Tags.DeliveryTerm);
            }
            public QuickFix.Fields.NoMDEntries NoMDEntries
            { 
                get 
                {
                    QuickFix.Fields.NoMDEntries val = new QuickFix.Fields.NoMDEntries();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.NoMDEntries val) 
            { 
                this.NoMDEntries = val;
            }
            
            public QuickFix.Fields.NoMDEntries Get(QuickFix.Fields.NoMDEntries val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.NoMDEntries val) 
            { 
                return IsSetNoMDEntries();
            }
            
            public bool IsSetNoMDEntries() 
            { 
                return IsSetField(Tags.NoMDEntries);
            }
            public class NoMDEntriesGroup : Group
            {
                public NoMDEntriesGroup() 
                  :base( Tags.NoMDEntries, Tags.MDEntryType, fieldOrder)
                {
                }
                public static int[] fieldOrder = {Tags.MDEntryType, Tags.MDEntryPx, Tags.MDEntrySize, Tags.MDEntryPositionNo, Tags.SecondaryOrderID, Tags.MDEntryState, Tags.AggressorSide, Tags.MDEntrySizeType, Tags.NumberOfOrders, Tags.ExactNumOrdersIndicator, Tags.TrdType, 0};
                            public QuickFix.Fields.MDEntryType MDEntryType
                { 
                    get 
                    {
                        QuickFix.Fields.MDEntryType val = new QuickFix.Fields.MDEntryType();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.MDEntryType val) 
                { 
                    this.MDEntryType = val;
                }
                
                public QuickFix.Fields.MDEntryType Get(QuickFix.Fields.MDEntryType val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.MDEntryType val) 
                { 
                    return IsSetMDEntryType();
                }
                
                public bool IsSetMDEntryType() 
                { 
                    return IsSetField(Tags.MDEntryType);
                }
                public QuickFix.Fields.MDEntryPx MDEntryPx
                { 
                    get 
                    {
                        QuickFix.Fields.MDEntryPx val = new QuickFix.Fields.MDEntryPx();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.MDEntryPx val) 
                { 
                    this.MDEntryPx = val;
                }
                
                public QuickFix.Fields.MDEntryPx Get(QuickFix.Fields.MDEntryPx val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.MDEntryPx val) 
                { 
                    return IsSetMDEntryPx();
                }
                
                public bool IsSetMDEntryPx() 
                { 
                    return IsSetField(Tags.MDEntryPx);
                }
                public QuickFix.Fields.MDEntrySize MDEntrySize
                { 
                    get 
                    {
                        QuickFix.Fields.MDEntrySize val = new QuickFix.Fields.MDEntrySize();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.MDEntrySize val) 
                { 
                    this.MDEntrySize = val;
                }
                
                public QuickFix.Fields.MDEntrySize Get(QuickFix.Fields.MDEntrySize val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.MDEntrySize val) 
                { 
                    return IsSetMDEntrySize();
                }
                
                public bool IsSetMDEntrySize() 
                { 
                    return IsSetField(Tags.MDEntrySize);
                }
                public QuickFix.Fields.MDEntryPositionNo MDEntryPositionNo
                { 
                    get 
                    {
                        QuickFix.Fields.MDEntryPositionNo val = new QuickFix.Fields.MDEntryPositionNo();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.MDEntryPositionNo val) 
                { 
                    this.MDEntryPositionNo = val;
                }
                
                public QuickFix.Fields.MDEntryPositionNo Get(QuickFix.Fields.MDEntryPositionNo val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.MDEntryPositionNo val) 
                { 
                    return IsSetMDEntryPositionNo();
                }
                
                public bool IsSetMDEntryPositionNo() 
                { 
                    return IsSetField(Tags.MDEntryPositionNo);
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
                public QuickFix.Fields.MDEntryState MDEntryState
                { 
                    get 
                    {
                        QuickFix.Fields.MDEntryState val = new QuickFix.Fields.MDEntryState();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.MDEntryState val) 
                { 
                    this.MDEntryState = val;
                }
                
                public QuickFix.Fields.MDEntryState Get(QuickFix.Fields.MDEntryState val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.MDEntryState val) 
                { 
                    return IsSetMDEntryState();
                }
                
                public bool IsSetMDEntryState() 
                { 
                    return IsSetField(Tags.MDEntryState);
                }
                public QuickFix.Fields.AggressorSide AggressorSide
                { 
                    get 
                    {
                        QuickFix.Fields.AggressorSide val = new QuickFix.Fields.AggressorSide();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.AggressorSide val) 
                { 
                    this.AggressorSide = val;
                }
                
                public QuickFix.Fields.AggressorSide Get(QuickFix.Fields.AggressorSide val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.AggressorSide val) 
                { 
                    return IsSetAggressorSide();
                }
                
                public bool IsSetAggressorSide() 
                { 
                    return IsSetField(Tags.AggressorSide);
                }
                public QuickFix.Fields.MDEntrySizeType MDEntrySizeType
                { 
                    get 
                    {
                        QuickFix.Fields.MDEntrySizeType val = new QuickFix.Fields.MDEntrySizeType();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.MDEntrySizeType val) 
                { 
                    this.MDEntrySizeType = val;
                }
                
                public QuickFix.Fields.MDEntrySizeType Get(QuickFix.Fields.MDEntrySizeType val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.MDEntrySizeType val) 
                { 
                    return IsSetMDEntrySizeType();
                }
                
                public bool IsSetMDEntrySizeType() 
                { 
                    return IsSetField(Tags.MDEntrySizeType);
                }
                public QuickFix.Fields.NumberOfOrders NumberOfOrders
                { 
                    get 
                    {
                        QuickFix.Fields.NumberOfOrders val = new QuickFix.Fields.NumberOfOrders();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.NumberOfOrders val) 
                { 
                    this.NumberOfOrders = val;
                }
                
                public QuickFix.Fields.NumberOfOrders Get(QuickFix.Fields.NumberOfOrders val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.NumberOfOrders val) 
                { 
                    return IsSetNumberOfOrders();
                }
                
                public bool IsSetNumberOfOrders() 
                { 
                    return IsSetField(Tags.NumberOfOrders);
                }
                public QuickFix.Fields.ExactNumOrdersIndicator ExactNumOrdersIndicator
                { 
                    get 
                    {
                        QuickFix.Fields.ExactNumOrdersIndicator val = new QuickFix.Fields.ExactNumOrdersIndicator();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.ExactNumOrdersIndicator val) 
                { 
                    this.ExactNumOrdersIndicator = val;
                }
                
                public QuickFix.Fields.ExactNumOrdersIndicator Get(QuickFix.Fields.ExactNumOrdersIndicator val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.ExactNumOrdersIndicator val) 
                { 
                    return IsSetExactNumOrdersIndicator();
                }
                
                public bool IsSetExactNumOrdersIndicator() 
                { 
                    return IsSetField(Tags.ExactNumOrdersIndicator);
                }
                public QuickFix.Fields.TrdType TrdType
                { 
                    get 
                    {
                        QuickFix.Fields.TrdType val = new QuickFix.Fields.TrdType();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.TrdType val) 
                { 
                    this.TrdType = val;
                }
                
                public QuickFix.Fields.TrdType Get(QuickFix.Fields.TrdType val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.TrdType val) 
                { 
                    return IsSetTrdType();
                }
                
                public bool IsSetTrdType() 
                { 
                    return IsSetField(Tags.TrdType);
                }
            
            }
        }
    }
}
