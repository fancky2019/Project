﻿// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42
    {
        public class MarketDataRequest : Message
        {
            public const string MsgType = "V";

            public MarketDataRequest() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("V"));
            }

            public MarketDataRequest(
                    QuickFix.Fields.MDReqID aMDReqID,
                    QuickFix.Fields.SubscriptionRequestType aSubscriptionRequestType
                ) : this()
            {
                this.MDReqID = aMDReqID;
                this.SubscriptionRequestType = aSubscriptionRequestType;
            }


            #region  fancky  add

            public QuickFix.Fields.OnBehalfOfSubID OnBehalfOfSubID
            {
                get
                {
                    QuickFix.Fields.OnBehalfOfSubID val = new QuickFix.Fields.OnBehalfOfSubID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.OnBehalfOfSubID val)
            {
                this.OnBehalfOfSubID = val;
            }

            public QuickFix.Fields.OnBehalfOfSubID Get(QuickFix.Fields.OnBehalfOfSubID val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.OnBehalfOfSubID val)
            {
                return IsSetOnBehalfOfSubID();
            }

            public bool IsSetOnBehalfOfSubID()
            {
                return IsSetField(Tags.OnBehalfOfSubID);
            }

            #endregion





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
            public QuickFix.Fields.MarketDepth MarketDepth
            {
                get
                {
                    QuickFix.Fields.MarketDepth val = new QuickFix.Fields.MarketDepth();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.MarketDepth val)
            {
                this.MarketDepth = val;
            }

            public QuickFix.Fields.MarketDepth Get(QuickFix.Fields.MarketDepth val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.MarketDepth val)
            {
                return IsSetMarketDepth();
            }

            public bool IsSetMarketDepth()
            {
                return IsSetField(Tags.MarketDepth);
            }
            public QuickFix.Fields.MDUpdateType MDUpdateType
            {
                get
                {
                    QuickFix.Fields.MDUpdateType val = new QuickFix.Fields.MDUpdateType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.MDUpdateType val)
            {
                this.MDUpdateType = val;
            }

            public QuickFix.Fields.MDUpdateType Get(QuickFix.Fields.MDUpdateType val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.MDUpdateType val)
            {
                return IsSetMDUpdateType();
            }

            public bool IsSetMDUpdateType()
            {
                return IsSetField(Tags.MDUpdateType);
            }
            public QuickFix.Fields.AggregatedBook AggregatedBook
            {
                get
                {
                    QuickFix.Fields.AggregatedBook val = new QuickFix.Fields.AggregatedBook();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.AggregatedBook val)
            {
                this.AggregatedBook = val;
            }

            public QuickFix.Fields.AggregatedBook Get(QuickFix.Fields.AggregatedBook val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.AggregatedBook val)
            {
                return IsSetAggregatedBook();
            }

            public bool IsSetAggregatedBook()
            {
                return IsSetField(Tags.AggregatedBook);
            }
            public QuickFix.Fields.NoMDEntryTypes NoMDEntryTypes
            {
                get
                {
                    QuickFix.Fields.NoMDEntryTypes val = new QuickFix.Fields.NoMDEntryTypes();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.NoMDEntryTypes val)
            {
                this.NoMDEntryTypes = val;
            }

            public QuickFix.Fields.NoMDEntryTypes Get(QuickFix.Fields.NoMDEntryTypes val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.NoMDEntryTypes val)
            {
                return IsSetNoMDEntryTypes();
            }

            public bool IsSetNoMDEntryTypes()
            {
                return IsSetField(Tags.NoMDEntryTypes);
            }
            public QuickFix.Fields.NoRelatedSym NoRelatedSym
            {
                get
                {
                    QuickFix.Fields.NoRelatedSym val = new QuickFix.Fields.NoRelatedSym();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.NoRelatedSym val)
            {
                this.NoRelatedSym = val;
            }

            public QuickFix.Fields.NoRelatedSym Get(QuickFix.Fields.NoRelatedSym val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.NoRelatedSym val)
            {
                return IsSetNoRelatedSym();
            }

            public bool IsSetNoRelatedSym()
            {
                return IsSetField(Tags.NoRelatedSym);
            }
            public QuickFix.Fields.IncludeNumberOfOrders IncludeNumberOfOrders
            {
                get
                {
                    QuickFix.Fields.IncludeNumberOfOrders val = new QuickFix.Fields.IncludeNumberOfOrders();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.IncludeNumberOfOrders val)
            {
                this.IncludeNumberOfOrders = val;
            }

            public QuickFix.Fields.IncludeNumberOfOrders Get(QuickFix.Fields.IncludeNumberOfOrders val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.IncludeNumberOfOrders val)
            {
                return IsSetIncludeNumberOfOrders();
            }

            public bool IsSetIncludeNumberOfOrders()
            {
                return IsSetField(Tags.IncludeNumberOfOrders);
            }
            public class NoMDEntryTypesGroup : Group
            {
                public static int[] fieldOrder = { Tags.MDEntryType, 0 };

                public NoMDEntryTypesGroup()
                  : base(Tags.NoMDEntryTypes, Tags.MDEntryType, fieldOrder)
                {
                }

                public override Group Clone()
                {
                    var clone = new NoMDEntryTypesGroup();
                    clone.CopyStateFrom(this);
                    return clone;
                }

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

            }
            public class NoRelatedSymGroup : Group
            {
                public static int[] fieldOrder = { Tags.Symbol, Tags.SecurityType, Tags.MaturityMonthYear, Tags.MaturityDate, Tags.MaturityDay, Tags.PutOrCall, Tags.StrikePrice, Tags.OptAttribute, Tags.DeliveryTerm, Tags.DeliveryDate, Tags.SecurityID, Tags.SecurityExchange, Tags.ExDestination, Tags.IDSource, Tags.NoSecurityAltID, Tags.NoLegs, 0 };

                public NoRelatedSymGroup()
                  : base(Tags.NoRelatedSym, Tags.Symbol, fieldOrder)
                {
                }

                public override Group Clone()
                {
                    var clone = new NoRelatedSymGroup();
                    clone.CopyStateFrom(this);
                    return clone;
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
                public QuickFix.Fields.MaturityDate MaturityDate
                {
                    get
                    {
                        QuickFix.Fields.MaturityDate val = new QuickFix.Fields.MaturityDate();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }

                public void Set(QuickFix.Fields.MaturityDate val)
                {
                    this.MaturityDate = val;
                }

                public QuickFix.Fields.MaturityDate Get(QuickFix.Fields.MaturityDate val)
                {
                    GetField(val);
                    return val;
                }

                public bool IsSet(QuickFix.Fields.MaturityDate val)
                {
                    return IsSetMaturityDate();
                }

                public bool IsSetMaturityDate()
                {
                    return IsSetField(Tags.MaturityDate);
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
                public QuickFix.Fields.DeliveryDate DeliveryDate
                {
                    get
                    {
                        QuickFix.Fields.DeliveryDate val = new QuickFix.Fields.DeliveryDate();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }

                public void Set(QuickFix.Fields.DeliveryDate val)
                {
                    this.DeliveryDate = val;
                }

                public QuickFix.Fields.DeliveryDate Get(QuickFix.Fields.DeliveryDate val)
                {
                    GetField(val);
                    return val;
                }

                public bool IsSet(QuickFix.Fields.DeliveryDate val)
                {
                    return IsSetDeliveryDate();
                }

                public bool IsSetDeliveryDate()
                {
                    return IsSetField(Tags.DeliveryDate);
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
                public QuickFix.Fields.ExDestination ExDestination
                {
                    get
                    {
                        QuickFix.Fields.ExDestination val = new QuickFix.Fields.ExDestination();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }

                public void Set(QuickFix.Fields.ExDestination val)
                {
                    this.ExDestination = val;
                }

                public QuickFix.Fields.ExDestination Get(QuickFix.Fields.ExDestination val)
                {
                    GetField(val);
                    return val;
                }

                public bool IsSet(QuickFix.Fields.ExDestination val)
                {
                    return IsSetExDestination();
                }

                public bool IsSetExDestination()
                {
                    return IsSetField(Tags.ExDestination);
                }
                public QuickFix.Fields.IDSource IDSource
                {
                    get
                    {
                        QuickFix.Fields.IDSource val = new QuickFix.Fields.IDSource();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }

                public void Set(QuickFix.Fields.IDSource val)
                {
                    this.IDSource = val;
                }

                public QuickFix.Fields.IDSource Get(QuickFix.Fields.IDSource val)
                {
                    GetField(val);
                    return val;
                }

                public bool IsSet(QuickFix.Fields.IDSource val)
                {
                    return IsSetIDSource();
                }

                public bool IsSetIDSource()
                {
                    return IsSetField(Tags.IDSource);
                }
                public QuickFix.Fields.NoSecurityAltID NoSecurityAltID
                {
                    get
                    {
                        QuickFix.Fields.NoSecurityAltID val = new QuickFix.Fields.NoSecurityAltID();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }

                public void Set(QuickFix.Fields.NoSecurityAltID val)
                {
                    this.NoSecurityAltID = val;
                }

                public QuickFix.Fields.NoSecurityAltID Get(QuickFix.Fields.NoSecurityAltID val)
                {
                    GetField(val);
                    return val;
                }

                public bool IsSet(QuickFix.Fields.NoSecurityAltID val)
                {
                    return IsSetNoSecurityAltID();
                }

                public bool IsSetNoSecurityAltID()
                {
                    return IsSetField(Tags.NoSecurityAltID);
                }
                public QuickFix.Fields.NoLegs NoLegs
                {
                    get
                    {
                        QuickFix.Fields.NoLegs val = new QuickFix.Fields.NoLegs();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }

                public void Set(QuickFix.Fields.NoLegs val)
                {
                    this.NoLegs = val;
                }

                public QuickFix.Fields.NoLegs Get(QuickFix.Fields.NoLegs val)
                {
                    GetField(val);
                    return val;
                }

                public bool IsSet(QuickFix.Fields.NoLegs val)
                {
                    return IsSetNoLegs();
                }

                public bool IsSetNoLegs()
                {
                    return IsSetField(Tags.NoLegs);
                }
                public class NoSecurityAltIDGroup : Group
                {
                    public static int[] fieldOrder = { Tags.SecurityAltID, Tags.SecurityAltIDSource, 0 };

                    public NoSecurityAltIDGroup()
                      : base(Tags.NoSecurityAltID, Tags.SecurityAltID, fieldOrder)
                    {
                    }

                    public override Group Clone()
                    {
                        var clone = new NoSecurityAltIDGroup();
                        clone.CopyStateFrom(this);
                        return clone;
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
                    public QuickFix.Fields.SecurityAltIDSource SecurityAltIDSource
                    {
                        get
                        {
                            QuickFix.Fields.SecurityAltIDSource val = new QuickFix.Fields.SecurityAltIDSource();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.SecurityAltIDSource val)
                    {
                        this.SecurityAltIDSource = val;
                    }

                    public QuickFix.Fields.SecurityAltIDSource Get(QuickFix.Fields.SecurityAltIDSource val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.SecurityAltIDSource val)
                    {
                        return IsSetSecurityAltIDSource();
                    }

                    public bool IsSetSecurityAltIDSource()
                    {
                        return IsSetField(Tags.SecurityAltIDSource);
                    }

                }
                public class NoLegsGroup : Group
                {
                    public static int[] fieldOrder = { Tags.LegSymbol, Tags.LegSecurityID, Tags.LegSecurityType, Tags.LegMaturityMonthYear, Tags.LegMaturityDate, Tags.LegMaturityDay, Tags.LegStrikePrice, Tags.LegPutOrCall, Tags.LegOptAttribute, Tags.LegSecurityExchange, Tags.LegExDestination, Tags.LegSide, Tags.LegRatioQty, Tags.LegCurrency, Tags.LegPrice, Tags.LegDeliveryTerm, Tags.LegDeliveryDate, Tags.NoLegSecurityAltID, 0 };

                    public NoLegsGroup()
                      : base(Tags.NoLegs, Tags.LegSymbol, fieldOrder)
                    {
                    }

                    public override Group Clone()
                    {
                        var clone = new NoLegsGroup();
                        clone.CopyStateFrom(this);
                        return clone;
                    }

                    public QuickFix.Fields.LegSymbol LegSymbol
                    {
                        get
                        {
                            QuickFix.Fields.LegSymbol val = new QuickFix.Fields.LegSymbol();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegSymbol val)
                    {
                        this.LegSymbol = val;
                    }

                    public QuickFix.Fields.LegSymbol Get(QuickFix.Fields.LegSymbol val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegSymbol val)
                    {
                        return IsSetLegSymbol();
                    }

                    public bool IsSetLegSymbol()
                    {
                        return IsSetField(Tags.LegSymbol);
                    }
                    public QuickFix.Fields.LegSecurityID LegSecurityID
                    {
                        get
                        {
                            QuickFix.Fields.LegSecurityID val = new QuickFix.Fields.LegSecurityID();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegSecurityID val)
                    {
                        this.LegSecurityID = val;
                    }

                    public QuickFix.Fields.LegSecurityID Get(QuickFix.Fields.LegSecurityID val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegSecurityID val)
                    {
                        return IsSetLegSecurityID();
                    }

                    public bool IsSetLegSecurityID()
                    {
                        return IsSetField(Tags.LegSecurityID);
                    }
                    public QuickFix.Fields.LegSecurityType LegSecurityType
                    {
                        get
                        {
                            QuickFix.Fields.LegSecurityType val = new QuickFix.Fields.LegSecurityType();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegSecurityType val)
                    {
                        this.LegSecurityType = val;
                    }

                    public QuickFix.Fields.LegSecurityType Get(QuickFix.Fields.LegSecurityType val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegSecurityType val)
                    {
                        return IsSetLegSecurityType();
                    }

                    public bool IsSetLegSecurityType()
                    {
                        return IsSetField(Tags.LegSecurityType);
                    }
                    public QuickFix.Fields.LegMaturityMonthYear LegMaturityMonthYear
                    {
                        get
                        {
                            QuickFix.Fields.LegMaturityMonthYear val = new QuickFix.Fields.LegMaturityMonthYear();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegMaturityMonthYear val)
                    {
                        this.LegMaturityMonthYear = val;
                    }

                    public QuickFix.Fields.LegMaturityMonthYear Get(QuickFix.Fields.LegMaturityMonthYear val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegMaturityMonthYear val)
                    {
                        return IsSetLegMaturityMonthYear();
                    }

                    public bool IsSetLegMaturityMonthYear()
                    {
                        return IsSetField(Tags.LegMaturityMonthYear);
                    }
                    public QuickFix.Fields.LegMaturityDate LegMaturityDate
                    {
                        get
                        {
                            QuickFix.Fields.LegMaturityDate val = new QuickFix.Fields.LegMaturityDate();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegMaturityDate val)
                    {
                        this.LegMaturityDate = val;
                    }

                    public QuickFix.Fields.LegMaturityDate Get(QuickFix.Fields.LegMaturityDate val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegMaturityDate val)
                    {
                        return IsSetLegMaturityDate();
                    }

                    public bool IsSetLegMaturityDate()
                    {
                        return IsSetField(Tags.LegMaturityDate);
                    }
                    public QuickFix.Fields.LegMaturityDay LegMaturityDay
                    {
                        get
                        {
                            QuickFix.Fields.LegMaturityDay val = new QuickFix.Fields.LegMaturityDay();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegMaturityDay val)
                    {
                        this.LegMaturityDay = val;
                    }

                    public QuickFix.Fields.LegMaturityDay Get(QuickFix.Fields.LegMaturityDay val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegMaturityDay val)
                    {
                        return IsSetLegMaturityDay();
                    }

                    public bool IsSetLegMaturityDay()
                    {
                        return IsSetField(Tags.LegMaturityDay);
                    }
                    public QuickFix.Fields.LegStrikePrice LegStrikePrice
                    {
                        get
                        {
                            QuickFix.Fields.LegStrikePrice val = new QuickFix.Fields.LegStrikePrice();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegStrikePrice val)
                    {
                        this.LegStrikePrice = val;
                    }

                    public QuickFix.Fields.LegStrikePrice Get(QuickFix.Fields.LegStrikePrice val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegStrikePrice val)
                    {
                        return IsSetLegStrikePrice();
                    }

                    public bool IsSetLegStrikePrice()
                    {
                        return IsSetField(Tags.LegStrikePrice);
                    }
                    public QuickFix.Fields.LegPutOrCall LegPutOrCall
                    {
                        get
                        {
                            QuickFix.Fields.LegPutOrCall val = new QuickFix.Fields.LegPutOrCall();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegPutOrCall val)
                    {
                        this.LegPutOrCall = val;
                    }

                    public QuickFix.Fields.LegPutOrCall Get(QuickFix.Fields.LegPutOrCall val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegPutOrCall val)
                    {
                        return IsSetLegPutOrCall();
                    }

                    public bool IsSetLegPutOrCall()
                    {
                        return IsSetField(Tags.LegPutOrCall);
                    }
                    public QuickFix.Fields.LegOptAttribute LegOptAttribute
                    {
                        get
                        {
                            QuickFix.Fields.LegOptAttribute val = new QuickFix.Fields.LegOptAttribute();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegOptAttribute val)
                    {
                        this.LegOptAttribute = val;
                    }

                    public QuickFix.Fields.LegOptAttribute Get(QuickFix.Fields.LegOptAttribute val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegOptAttribute val)
                    {
                        return IsSetLegOptAttribute();
                    }

                    public bool IsSetLegOptAttribute()
                    {
                        return IsSetField(Tags.LegOptAttribute);
                    }
                    public QuickFix.Fields.LegSecurityExchange LegSecurityExchange
                    {
                        get
                        {
                            QuickFix.Fields.LegSecurityExchange val = new QuickFix.Fields.LegSecurityExchange();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegSecurityExchange val)
                    {
                        this.LegSecurityExchange = val;
                    }

                    public QuickFix.Fields.LegSecurityExchange Get(QuickFix.Fields.LegSecurityExchange val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegSecurityExchange val)
                    {
                        return IsSetLegSecurityExchange();
                    }

                    public bool IsSetLegSecurityExchange()
                    {
                        return IsSetField(Tags.LegSecurityExchange);
                    }
                    public QuickFix.Fields.LegExDestination LegExDestination
                    {
                        get
                        {
                            QuickFix.Fields.LegExDestination val = new QuickFix.Fields.LegExDestination();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegExDestination val)
                    {
                        this.LegExDestination = val;
                    }

                    public QuickFix.Fields.LegExDestination Get(QuickFix.Fields.LegExDestination val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegExDestination val)
                    {
                        return IsSetLegExDestination();
                    }

                    public bool IsSetLegExDestination()
                    {
                        return IsSetField(Tags.LegExDestination);
                    }
                    public QuickFix.Fields.LegSide LegSide
                    {
                        get
                        {
                            QuickFix.Fields.LegSide val = new QuickFix.Fields.LegSide();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegSide val)
                    {
                        this.LegSide = val;
                    }

                    public QuickFix.Fields.LegSide Get(QuickFix.Fields.LegSide val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegSide val)
                    {
                        return IsSetLegSide();
                    }

                    public bool IsSetLegSide()
                    {
                        return IsSetField(Tags.LegSide);
                    }
                    public QuickFix.Fields.LegRatioQty LegRatioQty
                    {
                        get
                        {
                            QuickFix.Fields.LegRatioQty val = new QuickFix.Fields.LegRatioQty();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegRatioQty val)
                    {
                        this.LegRatioQty = val;
                    }

                    public QuickFix.Fields.LegRatioQty Get(QuickFix.Fields.LegRatioQty val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegRatioQty val)
                    {
                        return IsSetLegRatioQty();
                    }

                    public bool IsSetLegRatioQty()
                    {
                        return IsSetField(Tags.LegRatioQty);
                    }
                    public QuickFix.Fields.LegCurrency LegCurrency
                    {
                        get
                        {
                            QuickFix.Fields.LegCurrency val = new QuickFix.Fields.LegCurrency();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegCurrency val)
                    {
                        this.LegCurrency = val;
                    }

                    public QuickFix.Fields.LegCurrency Get(QuickFix.Fields.LegCurrency val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegCurrency val)
                    {
                        return IsSetLegCurrency();
                    }

                    public bool IsSetLegCurrency()
                    {
                        return IsSetField(Tags.LegCurrency);
                    }
                    public QuickFix.Fields.LegPrice LegPrice
                    {
                        get
                        {
                            QuickFix.Fields.LegPrice val = new QuickFix.Fields.LegPrice();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegPrice val)
                    {
                        this.LegPrice = val;
                    }

                    public QuickFix.Fields.LegPrice Get(QuickFix.Fields.LegPrice val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegPrice val)
                    {
                        return IsSetLegPrice();
                    }

                    public bool IsSetLegPrice()
                    {
                        return IsSetField(Tags.LegPrice);
                    }
                    public QuickFix.Fields.LegDeliveryTerm LegDeliveryTerm
                    {
                        get
                        {
                            QuickFix.Fields.LegDeliveryTerm val = new QuickFix.Fields.LegDeliveryTerm();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegDeliveryTerm val)
                    {
                        this.LegDeliveryTerm = val;
                    }

                    public QuickFix.Fields.LegDeliveryTerm Get(QuickFix.Fields.LegDeliveryTerm val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegDeliveryTerm val)
                    {
                        return IsSetLegDeliveryTerm();
                    }

                    public bool IsSetLegDeliveryTerm()
                    {
                        return IsSetField(Tags.LegDeliveryTerm);
                    }
                    public QuickFix.Fields.LegDeliveryDate LegDeliveryDate
                    {
                        get
                        {
                            QuickFix.Fields.LegDeliveryDate val = new QuickFix.Fields.LegDeliveryDate();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.LegDeliveryDate val)
                    {
                        this.LegDeliveryDate = val;
                    }

                    public QuickFix.Fields.LegDeliveryDate Get(QuickFix.Fields.LegDeliveryDate val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.LegDeliveryDate val)
                    {
                        return IsSetLegDeliveryDate();
                    }

                    public bool IsSetLegDeliveryDate()
                    {
                        return IsSetField(Tags.LegDeliveryDate);
                    }
                    public QuickFix.Fields.NoLegSecurityAltID NoLegSecurityAltID
                    {
                        get
                        {
                            QuickFix.Fields.NoLegSecurityAltID val = new QuickFix.Fields.NoLegSecurityAltID();
                            GetField(val);
                            return val;
                        }
                        set { SetField(value); }
                    }

                    public void Set(QuickFix.Fields.NoLegSecurityAltID val)
                    {
                        this.NoLegSecurityAltID = val;
                    }

                    public QuickFix.Fields.NoLegSecurityAltID Get(QuickFix.Fields.NoLegSecurityAltID val)
                    {
                        GetField(val);
                        return val;
                    }

                    public bool IsSet(QuickFix.Fields.NoLegSecurityAltID val)
                    {
                        return IsSetNoLegSecurityAltID();
                    }

                    public bool IsSetNoLegSecurityAltID()
                    {
                        return IsSetField(Tags.NoLegSecurityAltID);
                    }
                    public class NoLegSecurityAltIDGroup : Group
                    {
                        public static int[] fieldOrder = { Tags.LegSecurityAltID, Tags.LegSecurityAltIDSource, 0 };

                        public NoLegSecurityAltIDGroup()
                          : base(Tags.NoLegSecurityAltID, Tags.LegSecurityAltID, fieldOrder)
                        {
                        }

                        public override Group Clone()
                        {
                            var clone = new NoLegSecurityAltIDGroup();
                            clone.CopyStateFrom(this);
                            return clone;
                        }

                        public QuickFix.Fields.LegSecurityAltID LegSecurityAltID
                        {
                            get
                            {
                                QuickFix.Fields.LegSecurityAltID val = new QuickFix.Fields.LegSecurityAltID();
                                GetField(val);
                                return val;
                            }
                            set { SetField(value); }
                        }

                        public void Set(QuickFix.Fields.LegSecurityAltID val)
                        {
                            this.LegSecurityAltID = val;
                        }

                        public QuickFix.Fields.LegSecurityAltID Get(QuickFix.Fields.LegSecurityAltID val)
                        {
                            GetField(val);
                            return val;
                        }

                        public bool IsSet(QuickFix.Fields.LegSecurityAltID val)
                        {
                            return IsSetLegSecurityAltID();
                        }

                        public bool IsSetLegSecurityAltID()
                        {
                            return IsSetField(Tags.LegSecurityAltID);
                        }
                        public QuickFix.Fields.LegSecurityAltIDSource LegSecurityAltIDSource
                        {
                            get
                            {
                                QuickFix.Fields.LegSecurityAltIDSource val = new QuickFix.Fields.LegSecurityAltIDSource();
                                GetField(val);
                                return val;
                            }
                            set { SetField(value); }
                        }

                        public void Set(QuickFix.Fields.LegSecurityAltIDSource val)
                        {
                            this.LegSecurityAltIDSource = val;
                        }

                        public QuickFix.Fields.LegSecurityAltIDSource Get(QuickFix.Fields.LegSecurityAltIDSource val)
                        {
                            GetField(val);
                            return val;
                        }

                        public bool IsSet(QuickFix.Fields.LegSecurityAltIDSource val)
                        {
                            return IsSetLegSecurityAltIDSource();
                        }

                        public bool IsSetLegSecurityAltIDSource()
                        {
                            return IsSetField(Tags.LegSecurityAltIDSource);
                        }

                    }
                }
            }
        }
    }
}
