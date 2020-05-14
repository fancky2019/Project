// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class PositionReport : Message
        {
            public const string MsgType = "UAP";

            public PositionReport() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("UAP"));
            }

            public PositionReport(
                    QuickFix.Fields.PosReqId aPosReqId,
                    QuickFix.Fields.PosMaintRptId aPosMaintRptId,
                    QuickFix.Fields.TotalNumPosReports aTotalNumPosReports,
                    QuickFix.Fields.PosReqType aPosReqType
                ) : this()
            {
                this.PosReqId = aPosReqId;
                this.PosMaintRptId = aPosMaintRptId;
                this.TotalNumPosReports = aTotalNumPosReports;
                this.PosReqType = aPosReqType;
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
            public QuickFix.Fields.Price Price
            { 
                get 
                {
                    QuickFix.Fields.Price val = new QuickFix.Fields.Price();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.Price val) 
            { 
                this.Price = val;
            }
            
            public QuickFix.Fields.Price Get(QuickFix.Fields.Price val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.Price val) 
            { 
                return IsSetPrice();
            }
            
            public bool IsSetPrice() 
            { 
                return IsSetField(Tags.Price);
            }
            public QuickFix.Fields.StopPx StopPx
            { 
                get 
                {
                    QuickFix.Fields.StopPx val = new QuickFix.Fields.StopPx();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.StopPx val) 
            { 
                this.StopPx = val;
            }
            
            public QuickFix.Fields.StopPx Get(QuickFix.Fields.StopPx val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.StopPx val) 
            { 
                return IsSetStopPx();
            }
            
            public bool IsSetStopPx() 
            { 
                return IsSetField(Tags.StopPx);
            }
            public QuickFix.Fields.OrderQty OrderQty
            { 
                get 
                {
                    QuickFix.Fields.OrderQty val = new QuickFix.Fields.OrderQty();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OrderQty val) 
            { 
                this.OrderQty = val;
            }
            
            public QuickFix.Fields.OrderQty Get(QuickFix.Fields.OrderQty val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OrderQty val) 
            { 
                return IsSetOrderQty();
            }
            
            public bool IsSetOrderQty() 
            { 
                return IsSetField(Tags.OrderQty);
            }
            public QuickFix.Fields.MinQty MinQty
            { 
                get 
                {
                    QuickFix.Fields.MinQty val = new QuickFix.Fields.MinQty();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.MinQty val) 
            { 
                this.MinQty = val;
            }
            
            public QuickFix.Fields.MinQty Get(QuickFix.Fields.MinQty val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.MinQty val) 
            { 
                return IsSetMinQty();
            }
            
            public bool IsSetMinQty() 
            { 
                return IsSetField(Tags.MinQty);
            }
            public QuickFix.Fields.MaxShow MaxShow
            { 
                get 
                {
                    QuickFix.Fields.MaxShow val = new QuickFix.Fields.MaxShow();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.MaxShow val) 
            { 
                this.MaxShow = val;
            }
            
            public QuickFix.Fields.MaxShow Get(QuickFix.Fields.MaxShow val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.MaxShow val) 
            { 
                return IsSetMaxShow();
            }
            
            public bool IsSetMaxShow() 
            { 
                return IsSetField(Tags.MaxShow);
            }
            public QuickFix.Fields.Side Side
            { 
                get 
                {
                    QuickFix.Fields.Side val = new QuickFix.Fields.Side();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.Side val) 
            { 
                this.Side = val;
            }
            
            public QuickFix.Fields.Side Get(QuickFix.Fields.Side val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.Side val) 
            { 
                return IsSetSide();
            }
            
            public bool IsSetSide() 
            { 
                return IsSetField(Tags.Side);
            }
            public QuickFix.Fields.OrdType OrdType
            { 
                get 
                {
                    QuickFix.Fields.OrdType val = new QuickFix.Fields.OrdType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OrdType val) 
            { 
                this.OrdType = val;
            }
            
            public QuickFix.Fields.OrdType Get(QuickFix.Fields.OrdType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OrdType val) 
            { 
                return IsSetOrdType();
            }
            
            public bool IsSetOrdType() 
            { 
                return IsSetField(Tags.OrdType);
            }
            public QuickFix.Fields.OpenClose OpenClose
            { 
                get 
                {
                    QuickFix.Fields.OpenClose val = new QuickFix.Fields.OpenClose();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OpenClose val) 
            { 
                this.OpenClose = val;
            }
            
            public QuickFix.Fields.OpenClose Get(QuickFix.Fields.OpenClose val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OpenClose val) 
            { 
                return IsSetOpenClose();
            }
            
            public bool IsSetOpenClose() 
            { 
                return IsSetField(Tags.OpenClose);
            }
            public QuickFix.Fields.TimeInForce TimeInForce
            { 
                get 
                {
                    QuickFix.Fields.TimeInForce val = new QuickFix.Fields.TimeInForce();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TimeInForce val) 
            { 
                this.TimeInForce = val;
            }
            
            public QuickFix.Fields.TimeInForce Get(QuickFix.Fields.TimeInForce val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TimeInForce val) 
            { 
                return IsSetTimeInForce();
            }
            
            public bool IsSetTimeInForce() 
            { 
                return IsSetField(Tags.TimeInForce);
            }
            public QuickFix.Fields.ContraBroker ContraBroker
            { 
                get 
                {
                    QuickFix.Fields.ContraBroker val = new QuickFix.Fields.ContraBroker();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ContraBroker val) 
            { 
                this.ContraBroker = val;
            }
            
            public QuickFix.Fields.ContraBroker Get(QuickFix.Fields.ContraBroker val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ContraBroker val) 
            { 
                return IsSetContraBroker();
            }
            
            public bool IsSetContraBroker() 
            { 
                return IsSetField(Tags.ContraBroker);
            }
            public QuickFix.Fields.ExecBroker ExecBroker
            { 
                get 
                {
                    QuickFix.Fields.ExecBroker val = new QuickFix.Fields.ExecBroker();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ExecBroker val) 
            { 
                this.ExecBroker = val;
            }
            
            public QuickFix.Fields.ExecBroker Get(QuickFix.Fields.ExecBroker val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ExecBroker val) 
            { 
                return IsSetExecBroker();
            }
            
            public bool IsSetExecBroker() 
            { 
                return IsSetField(Tags.ExecBroker);
            }
            public QuickFix.Fields.ExpireDate ExpireDate
            { 
                get 
                {
                    QuickFix.Fields.ExpireDate val = new QuickFix.Fields.ExpireDate();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ExpireDate val) 
            { 
                this.ExpireDate = val;
            }
            
            public QuickFix.Fields.ExpireDate Get(QuickFix.Fields.ExpireDate val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ExpireDate val) 
            { 
                return IsSetExpireDate();
            }
            
            public bool IsSetExpireDate() 
            { 
                return IsSetField(Tags.ExpireDate);
            }
            public QuickFix.Fields.PassiveAggressive PassiveAggressive
            { 
                get 
                {
                    QuickFix.Fields.PassiveAggressive val = new QuickFix.Fields.PassiveAggressive();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.PassiveAggressive val) 
            { 
                this.PassiveAggressive = val;
            }
            
            public QuickFix.Fields.PassiveAggressive Get(QuickFix.Fields.PassiveAggressive val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.PassiveAggressive val) 
            { 
                return IsSetPassiveAggressive();
            }
            
            public bool IsSetPassiveAggressive() 
            { 
                return IsSetField(Tags.PassiveAggressive);
            }
            public QuickFix.Fields.AutoAggressive AutoAggressive
            { 
                get 
                {
                    QuickFix.Fields.AutoAggressive val = new QuickFix.Fields.AutoAggressive();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.AutoAggressive val) 
            { 
                this.AutoAggressive = val;
            }
            
            public QuickFix.Fields.AutoAggressive Get(QuickFix.Fields.AutoAggressive val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.AutoAggressive val) 
            { 
                return IsSetAutoAggressive();
            }
            
            public bool IsSetAutoAggressive() 
            { 
                return IsSetField(Tags.AutoAggressive);
            }
            public QuickFix.Fields.PosReqId PosReqId
            { 
                get 
                {
                    QuickFix.Fields.PosReqId val = new QuickFix.Fields.PosReqId();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.PosReqId val) 
            { 
                this.PosReqId = val;
            }
            
            public QuickFix.Fields.PosReqId Get(QuickFix.Fields.PosReqId val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.PosReqId val) 
            { 
                return IsSetPosReqId();
            }
            
            public bool IsSetPosReqId() 
            { 
                return IsSetField(Tags.PosReqId);
            }
            public QuickFix.Fields.PosMaintRptId PosMaintRptId
            { 
                get 
                {
                    QuickFix.Fields.PosMaintRptId val = new QuickFix.Fields.PosMaintRptId();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.PosMaintRptId val) 
            { 
                this.PosMaintRptId = val;
            }
            
            public QuickFix.Fields.PosMaintRptId Get(QuickFix.Fields.PosMaintRptId val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.PosMaintRptId val) 
            { 
                return IsSetPosMaintRptId();
            }
            
            public bool IsSetPosMaintRptId() 
            { 
                return IsSetField(Tags.PosMaintRptId);
            }
            public QuickFix.Fields.TotalNumPosReports TotalNumPosReports
            { 
                get 
                {
                    QuickFix.Fields.TotalNumPosReports val = new QuickFix.Fields.TotalNumPosReports();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TotalNumPosReports val) 
            { 
                this.TotalNumPosReports = val;
            }
            
            public QuickFix.Fields.TotalNumPosReports Get(QuickFix.Fields.TotalNumPosReports val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TotalNumPosReports val) 
            { 
                return IsSetTotalNumPosReports();
            }
            
            public bool IsSetTotalNumPosReports() 
            { 
                return IsSetField(Tags.TotalNumPosReports);
            }
            public QuickFix.Fields.PosReqType PosReqType
            { 
                get 
                {
                    QuickFix.Fields.PosReqType val = new QuickFix.Fields.PosReqType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.PosReqType val) 
            { 
                this.PosReqType = val;
            }
            
            public QuickFix.Fields.PosReqType Get(QuickFix.Fields.PosReqType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.PosReqType val) 
            { 
                return IsSetPosReqType();
            }
            
            public bool IsSetPosReqType() 
            { 
                return IsSetField(Tags.PosReqType);
            }
            public QuickFix.Fields.RealizedPandL RealizedPandL
            { 
                get 
                {
                    QuickFix.Fields.RealizedPandL val = new QuickFix.Fields.RealizedPandL();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.RealizedPandL val) 
            { 
                this.RealizedPandL = val;
            }
            
            public QuickFix.Fields.RealizedPandL Get(QuickFix.Fields.RealizedPandL val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.RealizedPandL val) 
            { 
                return IsSetRealizedPandL();
            }
            
            public bool IsSetRealizedPandL() 
            { 
                return IsSetField(Tags.RealizedPandL);
            }
            public QuickFix.Fields.LastShares LastShares
            { 
                get 
                {
                    QuickFix.Fields.LastShares val = new QuickFix.Fields.LastShares();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LastShares val) 
            { 
                this.LastShares = val;
            }
            
            public QuickFix.Fields.LastShares Get(QuickFix.Fields.LastShares val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LastShares val) 
            { 
                return IsSetLastShares();
            }
            
            public bool IsSetLastShares() 
            { 
                return IsSetField(Tags.LastShares);
            }
            public QuickFix.Fields.LastPx LastPx
            { 
                get 
                {
                    QuickFix.Fields.LastPx val = new QuickFix.Fields.LastPx();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LastPx val) 
            { 
                this.LastPx = val;
            }
            
            public QuickFix.Fields.LastPx Get(QuickFix.Fields.LastPx val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LastPx val) 
            { 
                return IsSetLastPx();
            }
            
            public bool IsSetLastPx() 
            { 
                return IsSetField(Tags.LastPx);
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
            public QuickFix.Fields.ExecID ExecID
            { 
                get 
                {
                    QuickFix.Fields.ExecID val = new QuickFix.Fields.ExecID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ExecID val) 
            { 
                this.ExecID = val;
            }
            
            public QuickFix.Fields.ExecID Get(QuickFix.Fields.ExecID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ExecID val) 
            { 
                return IsSetExecID();
            }
            
            public bool IsSetExecID() 
            { 
                return IsSetField(Tags.ExecID);
            }
            public QuickFix.Fields.SecondaryExecID SecondaryExecID
            { 
                get 
                {
                    QuickFix.Fields.SecondaryExecID val = new QuickFix.Fields.SecondaryExecID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.SecondaryExecID val) 
            { 
                this.SecondaryExecID = val;
            }
            
            public QuickFix.Fields.SecondaryExecID Get(QuickFix.Fields.SecondaryExecID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.SecondaryExecID val) 
            { 
                return IsSetSecondaryExecID();
            }
            
            public bool IsSetSecondaryExecID() 
            { 
                return IsSetField(Tags.SecondaryExecID);
            }
            public QuickFix.Fields.ExecRefID ExecRefID
            { 
                get 
                {
                    QuickFix.Fields.ExecRefID val = new QuickFix.Fields.ExecRefID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ExecRefID val) 
            { 
                this.ExecRefID = val;
            }
            
            public QuickFix.Fields.ExecRefID Get(QuickFix.Fields.ExecRefID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ExecRefID val) 
            { 
                return IsSetExecRefID();
            }
            
            public bool IsSetExecRefID() 
            { 
                return IsSetField(Tags.ExecRefID);
            }
            public QuickFix.Fields.ExecTransType ExecTransType
            { 
                get 
                {
                    QuickFix.Fields.ExecTransType val = new QuickFix.Fields.ExecTransType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ExecTransType val) 
            { 
                this.ExecTransType = val;
            }
            
            public QuickFix.Fields.ExecTransType Get(QuickFix.Fields.ExecTransType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ExecTransType val) 
            { 
                return IsSetExecTransType();
            }
            
            public bool IsSetExecTransType() 
            { 
                return IsSetField(Tags.ExecTransType);
            }
            public QuickFix.Fields.OrigExecID OrigExecID
            { 
                get 
                {
                    QuickFix.Fields.OrigExecID val = new QuickFix.Fields.OrigExecID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OrigExecID val) 
            { 
                this.OrigExecID = val;
            }
            
            public QuickFix.Fields.OrigExecID Get(QuickFix.Fields.OrigExecID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OrigExecID val) 
            { 
                return IsSetOrigExecID();
            }
            
            public bool IsSetOrigExecID() 
            { 
                return IsSetField(Tags.OrigExecID);
            }
            public QuickFix.Fields.TTSessionID TTSessionID
            { 
                get 
                {
                    QuickFix.Fields.TTSessionID val = new QuickFix.Fields.TTSessionID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTSessionID val) 
            { 
                this.TTSessionID = val;
            }
            
            public QuickFix.Fields.TTSessionID Get(QuickFix.Fields.TTSessionID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTSessionID val) 
            { 
                return IsSetTTSessionID();
            }
            
            public bool IsSetTTSessionID() 
            { 
                return IsSetField(Tags.TTSessionID);
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
            public QuickFix.Fields.MultiLegReportingType MultiLegReportingType
            { 
                get 
                {
                    QuickFix.Fields.MultiLegReportingType val = new QuickFix.Fields.MultiLegReportingType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.MultiLegReportingType val) 
            { 
                this.MultiLegReportingType = val;
            }
            
            public QuickFix.Fields.MultiLegReportingType Get(QuickFix.Fields.MultiLegReportingType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.MultiLegReportingType val) 
            { 
                return IsSetMultiLegReportingType();
            }
            
            public bool IsSetMultiLegReportingType() 
            { 
                return IsSetField(Tags.MultiLegReportingType);
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
            public QuickFix.Fields.SecuritySubType SecuritySubType
            { 
                get 
                {
                    QuickFix.Fields.SecuritySubType val = new QuickFix.Fields.SecuritySubType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.SecuritySubType val) 
            { 
                this.SecuritySubType = val;
            }
            
            public QuickFix.Fields.SecuritySubType Get(QuickFix.Fields.SecuritySubType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.SecuritySubType val) 
            { 
                return IsSetSecuritySubType();
            }
            
            public bool IsSetSecuritySubType() 
            { 
                return IsSetField(Tags.SecuritySubType);
            }
            public QuickFix.Fields.ClearingAccount ClearingAccount
            { 
                get 
                {
                    QuickFix.Fields.ClearingAccount val = new QuickFix.Fields.ClearingAccount();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ClearingAccount val) 
            { 
                this.ClearingAccount = val;
            }
            
            public QuickFix.Fields.ClearingAccount Get(QuickFix.Fields.ClearingAccount val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ClearingAccount val) 
            { 
                return IsSetClearingAccount();
            }
            
            public bool IsSetClearingAccount() 
            { 
                return IsSetField(Tags.ClearingAccount);
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
            public QuickFix.Fields.TTAccountType TTAccountType
            { 
                get 
                {
                    QuickFix.Fields.TTAccountType val = new QuickFix.Fields.TTAccountType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTAccountType val) 
            { 
                this.TTAccountType = val;
            }
            
            public QuickFix.Fields.TTAccountType Get(QuickFix.Fields.TTAccountType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTAccountType val) 
            { 
                return IsSetTTAccountType();
            }
            
            public bool IsSetTTAccountType() 
            { 
                return IsSetField(Tags.TTAccountType);
            }
            public QuickFix.Fields.TTUsername TTUsername
            { 
                get 
                {
                    QuickFix.Fields.TTUsername val = new QuickFix.Fields.TTUsername();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTUsername val) 
            { 
                this.TTUsername = val;
            }
            
            public QuickFix.Fields.TTUsername Get(QuickFix.Fields.TTUsername val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTUsername val) 
            { 
                return IsSetTTUsername();
            }
            
            public bool IsSetTTUsername() 
            { 
                return IsSetField(Tags.TTUsername);
            }
            
            public QuickFix.Fields.FFT2 FFT2
            { 
                get 
                {
                    QuickFix.Fields.FFT2 val = new QuickFix.Fields.FFT2();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.FFT2 val) 
            { 
                this.FFT2 = val;
            }
            
            public QuickFix.Fields.FFT2 Get(QuickFix.Fields.FFT2 val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.FFT2 val) 
            { 
                return IsSetFFT2();
            }
            
            public bool IsSetFFT2() 
            { 
                return IsSetField(Tags.FFT2);
            }
            public QuickFix.Fields.FFT3 FFT3
            { 
                get 
                {
                    QuickFix.Fields.FFT3 val = new QuickFix.Fields.FFT3();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.FFT3 val) 
            { 
                this.FFT3 = val;
            }
            
            public QuickFix.Fields.FFT3 Get(QuickFix.Fields.FFT3 val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.FFT3 val) 
            { 
                return IsSetFFT3();
            }
            
            public bool IsSetFFT3() 
            { 
                return IsSetField(Tags.FFT3);
            }
            public QuickFix.Fields.Rule80A Rule80A
            { 
                get 
                {
                    QuickFix.Fields.Rule80A val = new QuickFix.Fields.Rule80A();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.Rule80A val) 
            { 
                this.Rule80A = val;
            }
            
            public QuickFix.Fields.Rule80A Get(QuickFix.Fields.Rule80A val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.Rule80A val) 
            { 
                return IsSetRule80A();
            }
            
            public bool IsSetRule80A() 
            { 
                return IsSetField(Tags.Rule80A);
            }
            public QuickFix.Fields.CustomerOrFirm CustomerOrFirm
            { 
                get 
                {
                    QuickFix.Fields.CustomerOrFirm val = new QuickFix.Fields.CustomerOrFirm();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.CustomerOrFirm val) 
            { 
                this.CustomerOrFirm = val;
            }
            
            public QuickFix.Fields.CustomerOrFirm Get(QuickFix.Fields.CustomerOrFirm val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.CustomerOrFirm val) 
            { 
                return IsSetCustomerOrFirm();
            }
            
            public bool IsSetCustomerOrFirm() 
            { 
                return IsSetField(Tags.CustomerOrFirm);
            }
            public QuickFix.Fields.TradeCondition TradeCondition
            { 
                get 
                {
                    QuickFix.Fields.TradeCondition val = new QuickFix.Fields.TradeCondition();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TradeCondition val) 
            { 
                this.TradeCondition = val;
            }
            
            public QuickFix.Fields.TradeCondition Get(QuickFix.Fields.TradeCondition val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TradeCondition val) 
            { 
                return IsSetTradeCondition();
            }
            
            public bool IsSetTradeCondition() 
            { 
                return IsSetField(Tags.TradeCondition);
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
            public QuickFix.Fields.TradeDate TradeDate
            { 
                get 
                {
                    QuickFix.Fields.TradeDate val = new QuickFix.Fields.TradeDate();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TradeDate val) 
            { 
                this.TradeDate = val;
            }
            
            public QuickFix.Fields.TradeDate Get(QuickFix.Fields.TradeDate val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TradeDate val) 
            { 
                return IsSetTradeDate();
            }
            
            public bool IsSetTradeDate() 
            { 
                return IsSetField(Tags.TradeDate);
            }
            public QuickFix.Fields.FixingDate FixingDate
            { 
                get 
                {
                    QuickFix.Fields.FixingDate val = new QuickFix.Fields.FixingDate();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.FixingDate val) 
            { 
                this.FixingDate = val;
            }
            
            public QuickFix.Fields.FixingDate Get(QuickFix.Fields.FixingDate val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.FixingDate val) 
            { 
                return IsSetFixingDate();
            }
            
            public bool IsSetFixingDate() 
            { 
                return IsSetField(Tags.FixingDate);
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
            public QuickFix.Fields.UserTag UserTag
            { 
                get 
                {
                    QuickFix.Fields.UserTag val = new QuickFix.Fields.UserTag();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.UserTag val) 
            { 
                this.UserTag = val;
            }
            
            public QuickFix.Fields.UserTag Get(QuickFix.Fields.UserTag val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.UserTag val) 
            { 
                return IsSetUserTag();
            }
            
            public bool IsSetUserTag() 
            { 
                return IsSetField(Tags.UserTag);
            }
            public QuickFix.Fields.OrderTag OrderTag
            { 
                get 
                {
                    QuickFix.Fields.OrderTag val = new QuickFix.Fields.OrderTag();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OrderTag val) 
            { 
                this.OrderTag = val;
            }
            
            public QuickFix.Fields.OrderTag Get(QuickFix.Fields.OrderTag val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OrderTag val) 
            { 
                return IsSetOrderTag();
            }
            
            public bool IsSetOrderTag() 
            { 
                return IsSetField(Tags.OrderTag);
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
            public QuickFix.Fields.ExchCred ExchCred
            { 
                get 
                {
                    QuickFix.Fields.ExchCred val = new QuickFix.Fields.ExchCred();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ExchCred val) 
            { 
                this.ExchCred = val;
            }
            
            public QuickFix.Fields.ExchCred Get(QuickFix.Fields.ExchCred val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ExchCred val) 
            { 
                return IsSetExchCred();
            }
            
            public bool IsSetExchCred() 
            { 
                return IsSetField(Tags.ExchCred);
            }
            public QuickFix.Fields.TTStrategyEngine TTStrategyEngine
            { 
                get 
                {
                    QuickFix.Fields.TTStrategyEngine val = new QuickFix.Fields.TTStrategyEngine();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStrategyEngine val) 
            { 
                this.TTStrategyEngine = val;
            }
            
            public QuickFix.Fields.TTStrategyEngine Get(QuickFix.Fields.TTStrategyEngine val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStrategyEngine val) 
            { 
                return IsSetTTStrategyEngine();
            }
            
            public bool IsSetTTStrategyEngine() 
            { 
                return IsSetField(Tags.TTStrategyEngine);
            }
            public QuickFix.Fields.NoMiscFees NoMiscFees
            { 
                get 
                {
                    QuickFix.Fields.NoMiscFees val = new QuickFix.Fields.NoMiscFees();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.NoMiscFees val) 
            { 
                this.NoMiscFees = val;
            }
            
            public QuickFix.Fields.NoMiscFees Get(QuickFix.Fields.NoMiscFees val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.NoMiscFees val) 
            { 
                return IsSetNoMiscFees();
            }
            
            public bool IsSetNoMiscFees() 
            { 
                return IsSetField(Tags.NoMiscFees);
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
            public QuickFix.Fields.NoLinks NoLinks
            { 
                get 
                {
                    QuickFix.Fields.NoLinks val = new QuickFix.Fields.NoLinks();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.NoLinks val) 
            { 
                this.NoLinks = val;
            }
            
            public QuickFix.Fields.NoLinks Get(QuickFix.Fields.NoLinks val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.NoLinks val) 
            { 
                return IsSetNoLinks();
            }
            
            public bool IsSetNoLinks() 
            { 
                return IsSetField(Tags.NoLinks);
            }
            public class NoMiscFeesGroup : Group
            {
                public NoMiscFeesGroup() 
                  :base( Tags.NoMiscFees, Tags.MiscFeeAmt, fieldOrder)
                {
                }
                public static int[] fieldOrder = {Tags.MiscFeeAmt, 0};
                            public QuickFix.Fields.MiscFeeAmt MiscFeeAmt
                { 
                    get 
                    {
                        QuickFix.Fields.MiscFeeAmt val = new QuickFix.Fields.MiscFeeAmt();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.MiscFeeAmt val) 
                { 
                    this.MiscFeeAmt = val;
                }
                
                public QuickFix.Fields.MiscFeeAmt Get(QuickFix.Fields.MiscFeeAmt val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.MiscFeeAmt val) 
                { 
                    return IsSetMiscFeeAmt();
                }
                
                public bool IsSetMiscFeeAmt() 
                { 
                    return IsSetField(Tags.MiscFeeAmt);
                }
            
            }
            public class NoRelatedSymGroup : Group
            {
                public NoRelatedSymGroup() 
                  :base( Tags.NoRelatedSym, Tags.UnderlyingSymbol, fieldOrder)
                {
                }
                //public static int[] fieldOrder = {Tags.UnderlyingSymbol, Tags.UnderlyingSecurityID, Tags.UnderlyingSecurityType, Tags.UnderlyingMaturityMonthYear, Tags.UnderlyingStrikePrice, Tags.UnderlyingPutOrCall, Tags.UnderlyingMaturityDay, Tags.UnderlyingOptAttribute, Tags.UnderlyingSecurityExchange, Tags.UnderlyingDeliveryTerm, Tags.UnderlyingSecurityAltID, Tags.LegSide, Tags.RatioQty, Tags.UnderlyingCurrency, Tags.LegPrice, 0};
                public static int[] fieldOrder = { Tags.UnderlyingSymbol, Tags.UnderlyingSecurityID, Tags.UnderlyingSecurityType, Tags.UnderlyingMaturityMonthYear, Tags.UnderlyingStrikePrice, Tags.UnderlyingPutOrCall, Tags.UnderlyingMaturityDay, Tags.UnderlyingOptAttribute, Tags.UnderlyingSecurityExchange, Tags.UnderlyingSecurityAltID, Tags.LegSide, Tags.RatioQty, Tags.UnderlyingCurrency, Tags.LegPrice, 0 };
                public QuickFix.Fields.UnderlyingSymbol UnderlyingSymbol

                { 
                    get 
                    {
                        QuickFix.Fields.UnderlyingSymbol val = new QuickFix.Fields.UnderlyingSymbol();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.UnderlyingSymbol val) 
                { 
                    this.UnderlyingSymbol = val;
                }
                
                public QuickFix.Fields.UnderlyingSymbol Get(QuickFix.Fields.UnderlyingSymbol val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.UnderlyingSymbol val) 
                { 
                    return IsSetUnderlyingSymbol();
                }
                
                public bool IsSetUnderlyingSymbol() 
                { 
                    return IsSetField(Tags.UnderlyingSymbol);
                }
                public QuickFix.Fields.UnderlyingSecurityID UnderlyingSecurityID
                { 
                    get 
                    {
                        QuickFix.Fields.UnderlyingSecurityID val = new QuickFix.Fields.UnderlyingSecurityID();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.UnderlyingSecurityID val) 
                { 
                    this.UnderlyingSecurityID = val;
                }
                
                public QuickFix.Fields.UnderlyingSecurityID Get(QuickFix.Fields.UnderlyingSecurityID val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.UnderlyingSecurityID val) 
                { 
                    return IsSetUnderlyingSecurityID();
                }
                
                public bool IsSetUnderlyingSecurityID() 
                { 
                    return IsSetField(Tags.UnderlyingSecurityID);
                }
                public QuickFix.Fields.UnderlyingSecurityType UnderlyingSecurityType
                { 
                    get 
                    {
                        QuickFix.Fields.UnderlyingSecurityType val = new QuickFix.Fields.UnderlyingSecurityType();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.UnderlyingSecurityType val) 
                { 
                    this.UnderlyingSecurityType = val;
                }
                
                public QuickFix.Fields.UnderlyingSecurityType Get(QuickFix.Fields.UnderlyingSecurityType val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.UnderlyingSecurityType val) 
                { 
                    return IsSetUnderlyingSecurityType();
                }
                
                public bool IsSetUnderlyingSecurityType() 
                { 
                    return IsSetField(Tags.UnderlyingSecurityType);
                }
                public QuickFix.Fields.UnderlyingMaturityMonthYear UnderlyingMaturityMonthYear
                { 
                    get 
                    {
                        QuickFix.Fields.UnderlyingMaturityMonthYear val = new QuickFix.Fields.UnderlyingMaturityMonthYear();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.UnderlyingMaturityMonthYear val) 
                { 
                    this.UnderlyingMaturityMonthYear = val;
                }
                
                public QuickFix.Fields.UnderlyingMaturityMonthYear Get(QuickFix.Fields.UnderlyingMaturityMonthYear val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.UnderlyingMaturityMonthYear val) 
                { 
                    return IsSetUnderlyingMaturityMonthYear();
                }
                
                public bool IsSetUnderlyingMaturityMonthYear() 
                { 
                    return IsSetField(Tags.UnderlyingMaturityMonthYear);
                }
                public QuickFix.Fields.UnderlyingStrikePrice UnderlyingStrikePrice
                { 
                    get 
                    {
                        QuickFix.Fields.UnderlyingStrikePrice val = new QuickFix.Fields.UnderlyingStrikePrice();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.UnderlyingStrikePrice val) 
                { 
                    this.UnderlyingStrikePrice = val;
                }
                
                public QuickFix.Fields.UnderlyingStrikePrice Get(QuickFix.Fields.UnderlyingStrikePrice val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.UnderlyingStrikePrice val) 
                { 
                    return IsSetUnderlyingStrikePrice();
                }
                
                public bool IsSetUnderlyingStrikePrice() 
                { 
                    return IsSetField(Tags.UnderlyingStrikePrice);
                }
                public QuickFix.Fields.UnderlyingPutOrCall UnderlyingPutOrCall
                { 
                    get 
                    {
                        QuickFix.Fields.UnderlyingPutOrCall val = new QuickFix.Fields.UnderlyingPutOrCall();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.UnderlyingPutOrCall val) 
                { 
                    this.UnderlyingPutOrCall = val;
                }
                
                public QuickFix.Fields.UnderlyingPutOrCall Get(QuickFix.Fields.UnderlyingPutOrCall val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.UnderlyingPutOrCall val) 
                { 
                    return IsSetUnderlyingPutOrCall();
                }
                
                public bool IsSetUnderlyingPutOrCall() 
                { 
                    return IsSetField(Tags.UnderlyingPutOrCall);
                }
                public QuickFix.Fields.UnderlyingMaturityDay UnderlyingMaturityDay
                { 
                    get 
                    {
                        QuickFix.Fields.UnderlyingMaturityDay val = new QuickFix.Fields.UnderlyingMaturityDay();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.UnderlyingMaturityDay val) 
                { 
                    this.UnderlyingMaturityDay = val;
                }
                
                public QuickFix.Fields.UnderlyingMaturityDay Get(QuickFix.Fields.UnderlyingMaturityDay val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.UnderlyingMaturityDay val) 
                { 
                    return IsSetUnderlyingMaturityDay();
                }
                
                public bool IsSetUnderlyingMaturityDay() 
                { 
                    return IsSetField(Tags.UnderlyingMaturityDay);
                }
                public QuickFix.Fields.UnderlyingOptAttribute UnderlyingOptAttribute
                { 
                    get 
                    {
                        QuickFix.Fields.UnderlyingOptAttribute val = new QuickFix.Fields.UnderlyingOptAttribute();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.UnderlyingOptAttribute val) 
                { 
                    this.UnderlyingOptAttribute = val;
                }
                
                public QuickFix.Fields.UnderlyingOptAttribute Get(QuickFix.Fields.UnderlyingOptAttribute val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.UnderlyingOptAttribute val) 
                { 
                    return IsSetUnderlyingOptAttribute();
                }
                
                public bool IsSetUnderlyingOptAttribute() 
                { 
                    return IsSetField(Tags.UnderlyingOptAttribute);
                }
                public QuickFix.Fields.UnderlyingSecurityExchange UnderlyingSecurityExchange
                { 
                    get 
                    {
                        QuickFix.Fields.UnderlyingSecurityExchange val = new QuickFix.Fields.UnderlyingSecurityExchange();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.UnderlyingSecurityExchange val) 
                { 
                    this.UnderlyingSecurityExchange = val;
                }
                
                public QuickFix.Fields.UnderlyingSecurityExchange Get(QuickFix.Fields.UnderlyingSecurityExchange val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.UnderlyingSecurityExchange val) 
                { 
                    return IsSetUnderlyingSecurityExchange();
                }
                
                public bool IsSetUnderlyingSecurityExchange() 
                { 
                    return IsSetField(Tags.UnderlyingSecurityExchange);
                }
                //public QuickFix.Fields.UnderlyingDeliveryTerm UnderlyingDeliveryTerm
                //{ 
                //    get 
                //    {
                //        QuickFix.Fields.UnderlyingDeliveryTerm val = new QuickFix.Fields.UnderlyingDeliveryTerm();
                //        GetField(val);
                //        return val;
                //    }
                //    set { SetField(value); }
                //}
                
                //public void Set(QuickFix.Fields.UnderlyingDeliveryTerm val) 
                //{ 
                //    this.UnderlyingDeliveryTerm = val;
                //}
                
                //public QuickFix.Fields.UnderlyingDeliveryTerm Get(QuickFix.Fields.UnderlyingDeliveryTerm val) 
                //{ 
                //    GetField(val);
                //    return val;
                //}
                
                //public bool IsSet(QuickFix.Fields.UnderlyingDeliveryTerm val) 
                //{ 
                //    return IsSetUnderlyingDeliveryTerm();
                //}
                
                //public bool IsSetUnderlyingDeliveryTerm() 
                //{ 
                //    return IsSetField(Tags.UnderlyingDeliveryTerm);
                //}
                public QuickFix.Fields.UnderlyingSecurityAltID UnderlyingSecurityAltID
                { 
                    get 
                    {
                        QuickFix.Fields.UnderlyingSecurityAltID val = new QuickFix.Fields.UnderlyingSecurityAltID();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.UnderlyingSecurityAltID val) 
                { 
                    this.UnderlyingSecurityAltID = val;
                }
                
                public QuickFix.Fields.UnderlyingSecurityAltID Get(QuickFix.Fields.UnderlyingSecurityAltID val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.UnderlyingSecurityAltID val) 
                { 
                    return IsSetUnderlyingSecurityAltID();
                }
                
                public bool IsSetUnderlyingSecurityAltID() 
                { 
                    return IsSetField(Tags.UnderlyingSecurityAltID);
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
                public QuickFix.Fields.RatioQty RatioQty
                { 
                    get 
                    {
                        QuickFix.Fields.RatioQty val = new QuickFix.Fields.RatioQty();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.RatioQty val) 
                { 
                    this.RatioQty = val;
                }
                
                public QuickFix.Fields.RatioQty Get(QuickFix.Fields.RatioQty val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.RatioQty val) 
                { 
                    return IsSetRatioQty();
                }
                
                public bool IsSetRatioQty() 
                { 
                    return IsSetField(Tags.RatioQty);
                }
                public QuickFix.Fields.UnderlyingCurrency UnderlyingCurrency
                { 
                    get 
                    {
                        QuickFix.Fields.UnderlyingCurrency val = new QuickFix.Fields.UnderlyingCurrency();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.UnderlyingCurrency val) 
                { 
                    this.UnderlyingCurrency = val;
                }
                
                public QuickFix.Fields.UnderlyingCurrency Get(QuickFix.Fields.UnderlyingCurrency val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.UnderlyingCurrency val) 
                { 
                    return IsSetUnderlyingCurrency();
                }
                
                public bool IsSetUnderlyingCurrency() 
                { 
                    return IsSetField(Tags.UnderlyingCurrency);
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
            
            }
            public class NoLinksGroup : Group
            {
                public NoLinksGroup() 
                  :base( Tags.NoLinks, Tags.LinkID, fieldOrder)
                {
                }
                public static int[] fieldOrder = {Tags.LinkID, Tags.LinkType, 0};
                            public QuickFix.Fields.LinkID LinkID
                { 
                    get 
                    {
                        QuickFix.Fields.LinkID val = new QuickFix.Fields.LinkID();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.LinkID val) 
                { 
                    this.LinkID = val;
                }
                
                public QuickFix.Fields.LinkID Get(QuickFix.Fields.LinkID val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.LinkID val) 
                { 
                    return IsSetLinkID();
                }
                
                public bool IsSetLinkID() 
                { 
                    return IsSetField(Tags.LinkID);
                }
                public QuickFix.Fields.LinkType LinkType
                { 
                    get 
                    {
                        QuickFix.Fields.LinkType val = new QuickFix.Fields.LinkType();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.LinkType val) 
                { 
                    this.LinkType = val;
                }
                
                public QuickFix.Fields.LinkType Get(QuickFix.Fields.LinkType val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.LinkType val) 
                { 
                    return IsSetLinkType();
                }
                
                public bool IsSetLinkType() 
                { 
                    return IsSetField(Tags.LinkType);
                }
            
            }
        }
    }
}
