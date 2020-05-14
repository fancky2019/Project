// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class ExecutionReport : Message
        {
            public const string MsgType = "8";

            public ExecutionReport() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("8"));
            }

            public ExecutionReport(
                    QuickFix.Fields.OrderID aOrderID,
                    QuickFix.Fields.ExecID aExecID,
                    QuickFix.Fields.ExecTransType aExecTransType,
                    QuickFix.Fields.ExecType aExecType,
                    QuickFix.Fields.OrdStatus aOrdStatus,
                    QuickFix.Fields.Symbol aSymbol,
                    QuickFix.Fields.Side aSide,
                    QuickFix.Fields.LeavesQty aLeavesQty,
                    QuickFix.Fields.CumQty aCumQty,
                    QuickFix.Fields.AvgPx aAvgPx
                ) : this()
            {
                this.OrderID = aOrderID;
                this.ExecID = aExecID;
                this.ExecTransType = aExecTransType;
                this.ExecType = aExecType;
                this.OrdStatus = aOrdStatus;
                this.Symbol = aSymbol;
                this.Side = aSide;
                this.LeavesQty = aLeavesQty;
                this.CumQty = aCumQty;
                this.AvgPx = aAvgPx;
            }



            #region GHF Tags

            public QuickFix.Fields.SenderSubID SenderSubID
            {
                get
                {
                    QuickFix.Fields.SenderSubID val = new QuickFix.Fields.SenderSubID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.SenderSubID val)
            {
                this.SenderSubID = val;
            }

            public QuickFix.Fields.SenderSubID Get(QuickFix.Fields.SenderSubID val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.SenderSubID val)
            {
                return IsSetSenderSubID();
            }

            public bool IsSetSenderSubID()
            {
                return IsSetField(Tags.SenderSubID);
            }




            public QuickFix.Fields.PartyID PartyID
            {
                get
                {
                    QuickFix.Fields.PartyID val = new QuickFix.Fields.PartyID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.PartyID val)
            {
                this.PartyID = val;
            }

            public QuickFix.Fields.PartyID Get(QuickFix.Fields.PartyID val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.PartyID val)
            {
                return IsSetPartyID();
            }

            public bool IsSetPartyID()
            {
                return IsSetField(Tags.PartyID);
            }

            public QuickFix.Fields.SelfMatchPreventionID SelfMatchPreventionID
            {
                get
                {
                    QuickFix.Fields.SelfMatchPreventionID val = new QuickFix.Fields.SelfMatchPreventionID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.SelfMatchPreventionID val)
            {
                this.SelfMatchPreventionID = val;
            }

            public QuickFix.Fields.SelfMatchPreventionID Get(QuickFix.Fields.SelfMatchPreventionID val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.SelfMatchPreventionID val)
            {
                return IsSetSelfMatchPreventionID();
            }

            public bool IsSetSelfMatchPreventionID()
            {
                return IsSetField(Tags.SelfMatchPreventionID);
            }





            public QuickFix.Fields.SMPInstruction SMPInstruction
            {
                get
                {
                    QuickFix.Fields.SMPInstruction val = new QuickFix.Fields.SMPInstruction();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.SMPInstruction val)
            {
                this.SMPInstruction = val;
            }

            public QuickFix.Fields.SMPInstruction Get(QuickFix.Fields.SMPInstruction val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.SMPInstruction val)
            {
                return IsSetSMPInstruction();
            }

            public bool IsSetSMPInstruction()
            {
                return IsSetField(Tags.SMPInstruction);
            }



            public QuickFix.Fields.TotNoStrikes TotNoStrikes
            {
                get
                {
                    QuickFix.Fields.TotNoStrikes val = new QuickFix.Fields.TotNoStrikes();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.TotNoStrikes val)
            {
                this.TotNoStrikes = val;
            }

            public QuickFix.Fields.TotNoStrikes Get(QuickFix.Fields.TotNoStrikes val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.TotNoStrikes val)
            {
                return IsSetTotNoStrikes();
            }

            public bool IsSetTotNoStrikes()
            {
                return IsSetField(Tags.TotNoStrikes);
            }
            
            #endregion

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
            public QuickFix.Fields.SecondaryClOrdID SecondaryClOrdID
            { 
                get 
                {
                    QuickFix.Fields.SecondaryClOrdID val = new QuickFix.Fields.SecondaryClOrdID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.SecondaryClOrdID val) 
            { 
                this.SecondaryClOrdID = val;
            }
            
            public QuickFix.Fields.SecondaryClOrdID Get(QuickFix.Fields.SecondaryClOrdID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.SecondaryClOrdID val) 
            { 
                return IsSetSecondaryClOrdID();
            }
            
            public bool IsSetSecondaryClOrdID() 
            { 
                return IsSetField(Tags.SecondaryClOrdID);
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
            public QuickFix.Fields.NoPartyIDs NoPartyIDs
            { 
                get 
                {
                    QuickFix.Fields.NoPartyIDs val = new QuickFix.Fields.NoPartyIDs();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.NoPartyIDs val) 
            { 
                this.NoPartyIDs = val;
            }
            
            public QuickFix.Fields.NoPartyIDs Get(QuickFix.Fields.NoPartyIDs val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.NoPartyIDs val) 
            { 
                return IsSetNoPartyIDs();
            }
            
            public bool IsSetNoPartyIDs() 
            { 
                return IsSetField(Tags.NoPartyIDs);
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
            public QuickFix.Fields.ExecType ExecType
            { 
                get 
                {
                    QuickFix.Fields.ExecType val = new QuickFix.Fields.ExecType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ExecType val) 
            { 
                this.ExecType = val;
            }
            
            public QuickFix.Fields.ExecType Get(QuickFix.Fields.ExecType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ExecType val) 
            { 
                return IsSetExecType();
            }
            
            public bool IsSetExecType() 
            { 
                return IsSetField(Tags.ExecType);
            }
            public QuickFix.Fields.ExecInst ExecInst
            { 
                get 
                {
                    QuickFix.Fields.ExecInst val = new QuickFix.Fields.ExecInst();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ExecInst val) 
            { 
                this.ExecInst = val;
            }
            
            public QuickFix.Fields.ExecInst Get(QuickFix.Fields.ExecInst val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ExecInst val) 
            { 
                return IsSetExecInst();
            }
            
            public bool IsSetExecInst() 
            { 
                return IsSetField(Tags.ExecInst);
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
            public QuickFix.Fields.OrdRejReason OrdRejReason
            { 
                get 
                {
                    QuickFix.Fields.OrdRejReason val = new QuickFix.Fields.OrdRejReason();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OrdRejReason val) 
            { 
                this.OrdRejReason = val;
            }
            
            public QuickFix.Fields.OrdRejReason Get(QuickFix.Fields.OrdRejReason val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OrdRejReason val) 
            { 
                return IsSetOrdRejReason();
            }
            
            public bool IsSetOrdRejReason() 
            { 
                return IsSetField(Tags.OrdRejReason);
            }
            public QuickFix.Fields.ExecRestatementReason ExecRestatementReason
            { 
                get 
                {
                    QuickFix.Fields.ExecRestatementReason val = new QuickFix.Fields.ExecRestatementReason();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ExecRestatementReason val) 
            { 
                this.ExecRestatementReason = val;
            }
            
            public QuickFix.Fields.ExecRestatementReason Get(QuickFix.Fields.ExecRestatementReason val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ExecRestatementReason val) 
            { 
                return IsSetExecRestatementReason();
            }
            
            public bool IsSetExecRestatementReason() 
            { 
                return IsSetField(Tags.ExecRestatementReason);
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
            public QuickFix.Fields.LeavesQty LeavesQty
            { 
                get 
                {
                    QuickFix.Fields.LeavesQty val = new QuickFix.Fields.LeavesQty();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LeavesQty val) 
            { 
                this.LeavesQty = val;
            }
            
            public QuickFix.Fields.LeavesQty Get(QuickFix.Fields.LeavesQty val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LeavesQty val) 
            { 
                return IsSetLeavesQty();
            }
            
            public bool IsSetLeavesQty() 
            { 
                return IsSetField(Tags.LeavesQty);
            }
            public QuickFix.Fields.CumQty CumQty
            { 
                get 
                {
                    QuickFix.Fields.CumQty val = new QuickFix.Fields.CumQty();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.CumQty val) 
            { 
                this.CumQty = val;
            }
            
            public QuickFix.Fields.CumQty Get(QuickFix.Fields.CumQty val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.CumQty val) 
            { 
                return IsSetCumQty();
            }
            
            public bool IsSetCumQty() 
            { 
                return IsSetField(Tags.CumQty);
            }
            public QuickFix.Fields.AvgPx AvgPx
            { 
                get 
                {
                    QuickFix.Fields.AvgPx val = new QuickFix.Fields.AvgPx();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.AvgPx val) 
            { 
                this.AvgPx = val;
            }
            
            public QuickFix.Fields.AvgPx Get(QuickFix.Fields.AvgPx val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.AvgPx val) 
            { 
                return IsSetAvgPx();
            }
            
            public bool IsSetAvgPx() 
            { 
                return IsSetField(Tags.AvgPx);
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
            public QuickFix.Fields.DisplayQty DisplayQty
            { 
                get 
                {
                    QuickFix.Fields.DisplayQty val = new QuickFix.Fields.DisplayQty();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.DisplayQty val) 
            { 
                this.DisplayQty = val;
            }
            
            public QuickFix.Fields.DisplayQty Get(QuickFix.Fields.DisplayQty val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.DisplayQty val) 
            { 
                return IsSetDisplayQty();
            }
            
            public bool IsSetDisplayQty() 
            { 
                return IsSetField(Tags.DisplayQty);
            }
            public QuickFix.Fields.RefreshQty RefreshQty
            { 
                get 
                {
                    QuickFix.Fields.RefreshQty val = new QuickFix.Fields.RefreshQty();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.RefreshQty val) 
            { 
                this.RefreshQty = val;
            }
            
            public QuickFix.Fields.RefreshQty Get(QuickFix.Fields.RefreshQty val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.RefreshQty val) 
            { 
                return IsSetRefreshQty();
            }
            
            public bool IsSetRefreshQty() 
            { 
                return IsSetField(Tags.RefreshQty);
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
            public QuickFix.Fields.ManualOrderIndicator ManualOrderIndicator
            { 
                get 
                {
                    QuickFix.Fields.ManualOrderIndicator val = new QuickFix.Fields.ManualOrderIndicator();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ManualOrderIndicator val) 
            { 
                this.ManualOrderIndicator = val;
            }
            
            public QuickFix.Fields.ManualOrderIndicator Get(QuickFix.Fields.ManualOrderIndicator val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ManualOrderIndicator val) 
            { 
                return IsSetManualOrderIndicator();
            }
            
            public bool IsSetManualOrderIndicator() 
            { 
                return IsSetField(Tags.ManualOrderIndicator);
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
            public QuickFix.Fields.OrderCapacity OrderCapacity
            { 
                get 
                {
                    QuickFix.Fields.OrderCapacity val = new QuickFix.Fields.OrderCapacity();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OrderCapacity val) 
            { 
                this.OrderCapacity = val;
            }
            
            public QuickFix.Fields.OrderCapacity Get(QuickFix.Fields.OrderCapacity val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OrderCapacity val) 
            { 
                return IsSetOrderCapacity();
            }
            
            public bool IsSetOrderCapacity() 
            { 
                return IsSetField(Tags.OrderCapacity);
            }
            public QuickFix.Fields.CustOrderCapacity CustOrderCapacity
            { 
                get 
                {
                    QuickFix.Fields.CustOrderCapacity val = new QuickFix.Fields.CustOrderCapacity();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.CustOrderCapacity val) 
            { 
                this.CustOrderCapacity = val;
            }
            
            public QuickFix.Fields.CustOrderCapacity Get(QuickFix.Fields.CustOrderCapacity val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.CustOrderCapacity val) 
            { 
                return IsSetCustOrderCapacity();
            }
            
            public bool IsSetCustOrderCapacity() 
            { 
                return IsSetField(Tags.CustOrderCapacity);
            }
            public QuickFix.Fields.ContingencyType ContingencyType
            { 
                get 
                {
                    QuickFix.Fields.ContingencyType val = new QuickFix.Fields.ContingencyType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ContingencyType val) 
            { 
                this.ContingencyType = val;
            }
            
            public QuickFix.Fields.ContingencyType Get(QuickFix.Fields.ContingencyType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ContingencyType val) 
            { 
                return IsSetContingencyType();
            }
            
            public bool IsSetContingencyType() 
            { 
                return IsSetField(Tags.ContingencyType);
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
            public QuickFix.Fields.TrdMatchID TrdMatchID
            { 
                get 
                {
                    QuickFix.Fields.TrdMatchID val = new QuickFix.Fields.TrdMatchID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TrdMatchID val) 
            { 
                this.TrdMatchID = val;
            }
            
            public QuickFix.Fields.TrdMatchID Get(QuickFix.Fields.TrdMatchID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TrdMatchID val) 
            { 
                return IsSetTrdMatchID();
            }
            
            public bool IsSetTrdMatchID() 
            { 
                return IsSetField(Tags.TrdMatchID);
            }
            public QuickFix.Fields.CrossID CrossID
            { 
                get 
                {
                    QuickFix.Fields.CrossID val = new QuickFix.Fields.CrossID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.CrossID val) 
            { 
                this.CrossID = val;
            }
            
            public QuickFix.Fields.CrossID Get(QuickFix.Fields.CrossID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.CrossID val) 
            { 
                return IsSetCrossID();
            }
            
            public bool IsSetCrossID() 
            { 
                return IsSetField(Tags.CrossID);
            }
            public QuickFix.Fields.CrossType CrossType
            { 
                get 
                {
                    QuickFix.Fields.CrossType val = new QuickFix.Fields.CrossType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.CrossType val) 
            { 
                this.CrossType = val;
            }
            
            public QuickFix.Fields.CrossType Get(QuickFix.Fields.CrossType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.CrossType val) 
            { 
                return IsSetCrossType();
            }
            
            public bool IsSetCrossType() 
            { 
                return IsSetField(Tags.CrossType);
            }
            public QuickFix.Fields.TradeReportID TradeReportID
            { 
                get 
                {
                    QuickFix.Fields.TradeReportID val = new QuickFix.Fields.TradeReportID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TradeReportID val) 
            { 
                this.TradeReportID = val;
            }
            
            public QuickFix.Fields.TradeReportID Get(QuickFix.Fields.TradeReportID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TradeReportID val) 
            { 
                return IsSetTradeReportID();
            }
            
            public bool IsSetTradeReportID() 
            { 
                return IsSetField(Tags.TradeReportID);
            }
            public QuickFix.Fields.ComplianceID ComplianceID
            { 
                get 
                {
                    QuickFix.Fields.ComplianceID val = new QuickFix.Fields.ComplianceID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ComplianceID val) 
            { 
                this.ComplianceID = val;
            }
            
            public QuickFix.Fields.ComplianceID Get(QuickFix.Fields.ComplianceID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ComplianceID val) 
            { 
                return IsSetComplianceID();
            }
            
            public bool IsSetComplianceID() 
            { 
                return IsSetField(Tags.ComplianceID);
            }
            public QuickFix.Fields.SecondaryComplianceID SecondaryComplianceID
            { 
                get 
                {
                    QuickFix.Fields.SecondaryComplianceID val = new QuickFix.Fields.SecondaryComplianceID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.SecondaryComplianceID val) 
            { 
                this.SecondaryComplianceID = val;
            }
            
            public QuickFix.Fields.SecondaryComplianceID Get(QuickFix.Fields.SecondaryComplianceID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.SecondaryComplianceID val) 
            { 
                return IsSetSecondaryComplianceID();
            }
            
            public bool IsSetSecondaryComplianceID() 
            { 
                return IsSetField(Tags.SecondaryComplianceID);
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
            public QuickFix.Fields.TotalNumOrders TotalNumOrders
            { 
                get 
                {
                    QuickFix.Fields.TotalNumOrders val = new QuickFix.Fields.TotalNumOrders();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TotalNumOrders val) 
            { 
                this.TotalNumOrders = val;
            }
            
            public QuickFix.Fields.TotalNumOrders Get(QuickFix.Fields.TotalNumOrders val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TotalNumOrders val) 
            { 
                return IsSetTotalNumOrders();
            }
            
            public bool IsSetTotalNumOrders() 
            { 
                return IsSetField(Tags.TotalNumOrders);
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
            public QuickFix.Fields.LastParPx LastParPx
            { 
                get 
                {
                    QuickFix.Fields.LastParPx val = new QuickFix.Fields.LastParPx();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LastParPx val) 
            { 
                this.LastParPx = val;
            }
            
            public QuickFix.Fields.LastParPx Get(QuickFix.Fields.LastParPx val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LastParPx val) 
            { 
                return IsSetLastParPx();
            }
            
            public bool IsSetLastParPx() 
            { 
                return IsSetField(Tags.LastParPx);
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
            public QuickFix.Fields.ContractYearMonth ContractYearMonth
            { 
                get 
                {
                    QuickFix.Fields.ContractYearMonth val = new QuickFix.Fields.ContractYearMonth();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ContractYearMonth val) 
            { 
                this.ContractYearMonth = val;
            }
            
            public QuickFix.Fields.ContractYearMonth Get(QuickFix.Fields.ContractYearMonth val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ContractYearMonth val) 
            { 
                return IsSetContractYearMonth();
            }
            
            public bool IsSetContractYearMonth() 
            { 
                return IsSetField(Tags.ContractYearMonth);
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
            public QuickFix.Fields.AggressorIndicator AggressorIndicator
            { 
                get 
                {
                    QuickFix.Fields.AggressorIndicator val = new QuickFix.Fields.AggressorIndicator();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.AggressorIndicator val) 
            { 
                this.AggressorIndicator = val;
            }
            
            public QuickFix.Fields.AggressorIndicator Get(QuickFix.Fields.AggressorIndicator val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.AggressorIndicator val) 
            { 
                return IsSetAggressorIndicator();
            }
            
            public bool IsSetAggressorIndicator() 
            { 
                return IsSetField(Tags.AggressorIndicator);
            }
            public QuickFix.Fields.LegAllocID LegAllocID
            { 
                get 
                {
                    QuickFix.Fields.LegAllocID val = new QuickFix.Fields.LegAllocID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LegAllocID val) 
            { 
                this.LegAllocID = val;
            }
            
            public QuickFix.Fields.LegAllocID Get(QuickFix.Fields.LegAllocID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LegAllocID val) 
            { 
                return IsSetLegAllocID();
            }
            
            public bool IsSetLegAllocID() 
            { 
                return IsSetField(Tags.LegAllocID);
            }
            public QuickFix.Fields.EffectiveTime EffectiveTime
            { 
                get 
                {
                    QuickFix.Fields.EffectiveTime val = new QuickFix.Fields.EffectiveTime();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.EffectiveTime val) 
            { 
                this.EffectiveTime = val;
            }
            
            public QuickFix.Fields.EffectiveTime Get(QuickFix.Fields.EffectiveTime val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.EffectiveTime val) 
            { 
                return IsSetEffectiveTime();
            }
            
            public bool IsSetEffectiveTime() 
            { 
                return IsSetField(Tags.EffectiveTime);
            }
            public QuickFix.Fields.ExpireTime ExpireTime
            { 
                get 
                {
                    QuickFix.Fields.ExpireTime val = new QuickFix.Fields.ExpireTime();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ExpireTime val) 
            { 
                this.ExpireTime = val;
            }
            
            public QuickFix.Fields.ExpireTime Get(QuickFix.Fields.ExpireTime val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ExpireTime val) 
            { 
                return IsSetExpireTime();
            }
            
            public bool IsSetExpireTime() 
            { 
                return IsSetField(Tags.ExpireTime);
            }
            public QuickFix.Fields.TargetStrategyName TargetStrategyName
            { 
                get 
                {
                    QuickFix.Fields.TargetStrategyName val = new QuickFix.Fields.TargetStrategyName();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TargetStrategyName val) 
            { 
                this.TargetStrategyName = val;
            }
            
            public QuickFix.Fields.TargetStrategyName Get(QuickFix.Fields.TargetStrategyName val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TargetStrategyName val) 
            { 
                return IsSetTargetStrategyName();
            }
            
            public bool IsSetTargetStrategyName() 
            { 
                return IsSetField(Tags.TargetStrategyName);
            }
            public QuickFix.Fields.TargetStrategyType TargetStrategyType
            { 
                get 
                {
                    QuickFix.Fields.TargetStrategyType val = new QuickFix.Fields.TargetStrategyType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TargetStrategyType val) 
            { 
                this.TargetStrategyType = val;
            }
            
            public QuickFix.Fields.TargetStrategyType Get(QuickFix.Fields.TargetStrategyType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TargetStrategyType val) 
            { 
                return IsSetTargetStrategyType();
            }
            
            public bool IsSetTargetStrategyType() 
            { 
                return IsSetField(Tags.TargetStrategyType);
            }
            public QuickFix.Fields.BracketOrderType BracketOrderType
            { 
                get 
                {
                    QuickFix.Fields.BracketOrderType val = new QuickFix.Fields.BracketOrderType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.BracketOrderType val) 
            { 
                this.BracketOrderType = val;
            }
            
            public QuickFix.Fields.BracketOrderType Get(QuickFix.Fields.BracketOrderType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.BracketOrderType val) 
            { 
                return IsSetBracketOrderType();
            }
            
            public bool IsSetBracketOrderType() 
            { 
                return IsSetField(Tags.BracketOrderType);
            }
            public QuickFix.Fields.BracketStopLimitOffset BracketStopLimitOffset
            { 
                get 
                {
                    QuickFix.Fields.BracketStopLimitOffset val = new QuickFix.Fields.BracketStopLimitOffset();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.BracketStopLimitOffset val) 
            { 
                this.BracketStopLimitOffset = val;
            }
            
            public QuickFix.Fields.BracketStopLimitOffset Get(QuickFix.Fields.BracketStopLimitOffset val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.BracketStopLimitOffset val) 
            { 
                return IsSetBracketStopLimitOffset();
            }
            
            public bool IsSetBracketStopLimitOffset() 
            { 
                return IsSetField(Tags.BracketStopLimitOffset);
            }
            public QuickFix.Fields.ChildTIF ChildTIF
            { 
                get 
                {
                    QuickFix.Fields.ChildTIF val = new QuickFix.Fields.ChildTIF();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ChildTIF val) 
            { 
                this.ChildTIF = val;
            }
            
            public QuickFix.Fields.ChildTIF Get(QuickFix.Fields.ChildTIF val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ChildTIF val) 
            { 
                return IsSetChildTIF();
            }
            
            public bool IsSetChildTIF() 
            { 
                return IsSetField(Tags.ChildTIF);
            }
            public QuickFix.Fields.DiscVal DiscVal
            { 
                get 
                {
                    QuickFix.Fields.DiscVal val = new QuickFix.Fields.DiscVal();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.DiscVal val) 
            { 
                this.DiscVal = val;
            }
            
            public QuickFix.Fields.DiscVal Get(QuickFix.Fields.DiscVal val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.DiscVal val) 
            { 
                return IsSetDiscVal();
            }
            
            public bool IsSetDiscVal() 
            { 
                return IsSetField(Tags.DiscVal);
            }
            public QuickFix.Fields.DiscValType DiscValType
            { 
                get 
                {
                    QuickFix.Fields.DiscValType val = new QuickFix.Fields.DiscValType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.DiscValType val) 
            { 
                this.DiscValType = val;
            }
            
            public QuickFix.Fields.DiscValType Get(QuickFix.Fields.DiscValType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.DiscValType val) 
            { 
                return IsSetDiscValType();
            }
            
            public bool IsSetDiscValType() 
            { 
                return IsSetField(Tags.DiscValType);
            }
            public QuickFix.Fields.ETimeAct ETimeAct
            { 
                get 
                {
                    QuickFix.Fields.ETimeAct val = new QuickFix.Fields.ETimeAct();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ETimeAct val) 
            { 
                this.ETimeAct = val;
            }
            
            public QuickFix.Fields.ETimeAct Get(QuickFix.Fields.ETimeAct val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ETimeAct val) 
            { 
                return IsSetETimeAct();
            }
            
            public bool IsSetETimeAct() 
            { 
                return IsSetField(Tags.ETimeAct);
            }
            public QuickFix.Fields.Interval Interval
            { 
                get 
                {
                    QuickFix.Fields.Interval val = new QuickFix.Fields.Interval();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.Interval val) 
            { 
                this.Interval = val;
            }
            
            public QuickFix.Fields.Interval Get(QuickFix.Fields.Interval val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.Interval val) 
            { 
                return IsSetInterval();
            }
            
            public bool IsSetInterval() 
            { 
                return IsSetField(Tags.Interval);
            }
            public QuickFix.Fields.IsTrlTrg IsTrlTrg
            { 
                get 
                {
                    QuickFix.Fields.IsTrlTrg val = new QuickFix.Fields.IsTrlTrg();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.IsTrlTrg val) 
            { 
                this.IsTrlTrg = val;
            }
            
            public QuickFix.Fields.IsTrlTrg Get(QuickFix.Fields.IsTrlTrg val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.IsTrlTrg val) 
            { 
                return IsSetIsTrlTrg();
            }
            
            public bool IsSetIsTrlTrg() 
            { 
                return IsSetField(Tags.IsTrlTrg);
            }
            public QuickFix.Fields.LeftoverAction LeftoverAction
            { 
                get 
                {
                    QuickFix.Fields.LeftoverAction val = new QuickFix.Fields.LeftoverAction();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LeftoverAction val) 
            { 
                this.LeftoverAction = val;
            }
            
            public QuickFix.Fields.LeftoverAction Get(QuickFix.Fields.LeftoverAction val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LeftoverAction val) 
            { 
                return IsSetLeftoverAction();
            }
            
            public bool IsSetLeftoverAction() 
            { 
                return IsSetField(Tags.LeftoverAction);
            }
            public QuickFix.Fields.LeftoverTicks LeftoverTicks
            { 
                get 
                {
                    QuickFix.Fields.LeftoverTicks val = new QuickFix.Fields.LeftoverTicks();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LeftoverTicks val) 
            { 
                this.LeftoverTicks = val;
            }
            
            public QuickFix.Fields.LeftoverTicks Get(QuickFix.Fields.LeftoverTicks val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LeftoverTicks val) 
            { 
                return IsSetLeftoverTicks();
            }
            
            public bool IsSetLeftoverTicks() 
            { 
                return IsSetField(Tags.LeftoverTicks);
            }
            public QuickFix.Fields.LimitPriceType LimitPriceType
            { 
                get 
                {
                    QuickFix.Fields.LimitPriceType val = new QuickFix.Fields.LimitPriceType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LimitPriceType val) 
            { 
                this.LimitPriceType = val;
            }
            
            public QuickFix.Fields.LimitPriceType Get(QuickFix.Fields.LimitPriceType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LimitPriceType val) 
            { 
                return IsSetLimitPriceType();
            }
            
            public bool IsSetLimitPriceType() 
            { 
                return IsSetField(Tags.LimitPriceType);
            }
            public QuickFix.Fields.LimitTicksAway LimitTicksAway
            { 
                get 
                {
                    QuickFix.Fields.LimitTicksAway val = new QuickFix.Fields.LimitTicksAway();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LimitTicksAway val) 
            { 
                this.LimitTicksAway = val;
            }
            
            public QuickFix.Fields.LimitTicksAway Get(QuickFix.Fields.LimitTicksAway val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LimitTicksAway val) 
            { 
                return IsSetLimitTicksAway();
            }
            
            public bool IsSetLimitTicksAway() 
            { 
                return IsSetField(Tags.LimitTicksAway);
            }
            public QuickFix.Fields.OcoStopTriggerPrice OcoStopTriggerPrice
            { 
                get 
                {
                    QuickFix.Fields.OcoStopTriggerPrice val = new QuickFix.Fields.OcoStopTriggerPrice();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OcoStopTriggerPrice val) 
            { 
                this.OcoStopTriggerPrice = val;
            }
            
            public QuickFix.Fields.OcoStopTriggerPrice Get(QuickFix.Fields.OcoStopTriggerPrice val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OcoStopTriggerPrice val) 
            { 
                return IsSetOcoStopTriggerPrice();
            }
            
            public bool IsSetOcoStopTriggerPrice() 
            { 
                return IsSetField(Tags.OcoStopTriggerPrice);
            }
            public QuickFix.Fields.ProfitTarget ProfitTarget
            { 
                get 
                {
                    QuickFix.Fields.ProfitTarget val = new QuickFix.Fields.ProfitTarget();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.ProfitTarget val) 
            { 
                this.ProfitTarget = val;
            }
            
            public QuickFix.Fields.ProfitTarget Get(QuickFix.Fields.ProfitTarget val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.ProfitTarget val) 
            { 
                return IsSetProfitTarget();
            }
            
            public bool IsSetProfitTarget() 
            { 
                return IsSetField(Tags.ProfitTarget);
            }
            public QuickFix.Fields.StopLimitOffset StopLimitOffset
            { 
                get 
                {
                    QuickFix.Fields.StopLimitOffset val = new QuickFix.Fields.StopLimitOffset();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.StopLimitOffset val) 
            { 
                this.StopLimitOffset = val;
            }
            
            public QuickFix.Fields.StopLimitOffset Get(QuickFix.Fields.StopLimitOffset val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.StopLimitOffset val) 
            { 
                return IsSetStopLimitOffset();
            }
            
            public bool IsSetStopLimitOffset() 
            { 
                return IsSetField(Tags.StopLimitOffset);
            }
            public QuickFix.Fields.StopOrderType StopOrderType
            { 
                get 
                {
                    QuickFix.Fields.StopOrderType val = new QuickFix.Fields.StopOrderType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.StopOrderType val) 
            { 
                this.StopOrderType = val;
            }
            
            public QuickFix.Fields.StopOrderType Get(QuickFix.Fields.StopOrderType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.StopOrderType val) 
            { 
                return IsSetStopOrderType();
            }
            
            public bool IsSetStopOrderType() 
            { 
                return IsSetField(Tags.StopOrderType);
            }
            public QuickFix.Fields.StopTarget StopTarget
            { 
                get 
                {
                    QuickFix.Fields.StopTarget val = new QuickFix.Fields.StopTarget();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.StopTarget val) 
            { 
                this.StopTarget = val;
            }
            
            public QuickFix.Fields.StopTarget Get(QuickFix.Fields.StopTarget val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.StopTarget val) 
            { 
                return IsSetStopTarget();
            }
            
            public bool IsSetStopTarget() 
            { 
                return IsSetField(Tags.StopTarget);
            }
            public QuickFix.Fields.TriggerPriceType TriggerPriceType
            { 
                get 
                {
                    QuickFix.Fields.TriggerPriceType val = new QuickFix.Fields.TriggerPriceType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TriggerPriceType val) 
            { 
                this.TriggerPriceType = val;
            }
            
            public QuickFix.Fields.TriggerPriceType Get(QuickFix.Fields.TriggerPriceType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TriggerPriceType val) 
            { 
                return IsSetTriggerPriceType();
            }
            
            public bool IsSetTriggerPriceType() 
            { 
                return IsSetField(Tags.TriggerPriceType);
            }
            public QuickFix.Fields.TriggerTicksAway TriggerTicksAway
            { 
                get 
                {
                    QuickFix.Fields.TriggerTicksAway val = new QuickFix.Fields.TriggerTicksAway();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TriggerTicksAway val) 
            { 
                this.TriggerTicksAway = val;
            }
            
            public QuickFix.Fields.TriggerTicksAway Get(QuickFix.Fields.TriggerTicksAway val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TriggerTicksAway val) 
            { 
                return IsSetTriggerTicksAway();
            }
            
            public bool IsSetTriggerTicksAway() 
            { 
                return IsSetField(Tags.TriggerTicksAway);
            }
            public QuickFix.Fields.TriggerType TriggerType
            { 
                get 
                {
                    QuickFix.Fields.TriggerType val = new QuickFix.Fields.TriggerType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TriggerType val) 
            { 
                this.TriggerType = val;
            }
            
            public QuickFix.Fields.TriggerType Get(QuickFix.Fields.TriggerType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TriggerType val) 
            { 
                return IsSetTriggerType();
            }
            
            public bool IsSetTriggerType() 
            { 
                return IsSetField(Tags.TriggerType);
            }
            public QuickFix.Fields.WithATickType WithATickType
            { 
                get 
                {
                    QuickFix.Fields.WithATickType val = new QuickFix.Fields.WithATickType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.WithATickType val) 
            { 
                this.WithATickType = val;
            }
            
            public QuickFix.Fields.WithATickType Get(QuickFix.Fields.WithATickType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.WithATickType val) 
            { 
                return IsSetWithATickType();
            }
            
            public bool IsSetWithATickType() 
            { 
                return IsSetField(Tags.WithATickType);
            }
            public QuickFix.Fields.WithATick WithATick
            { 
                get 
                {
                    QuickFix.Fields.WithATick val = new QuickFix.Fields.WithATick();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.WithATick val) 
            { 
                this.WithATick = val;
            }
            
            public QuickFix.Fields.WithATick Get(QuickFix.Fields.WithATick val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.WithATick val) 
            { 
                return IsSetWithATick();
            }
            
            public bool IsSetWithATick() 
            { 
                return IsSetField(Tags.WithATick);
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
            public QuickFix.Fields.NoFills NoFills
            { 
                get 
                {
                    QuickFix.Fields.NoFills val = new QuickFix.Fields.NoFills();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.NoFills val) 
            { 
                this.NoFills = val;
            }
            
            public QuickFix.Fields.NoFills Get(QuickFix.Fields.NoFills val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.NoFills val) 
            { 
                return IsSetNoFills();
            }
            
            public bool IsSetNoFills() 
            { 
                return IsSetField(Tags.NoFills);
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
            public QuickFix.Fields.DropCopyOrder DropCopyOrder
            { 
                get 
                {
                    QuickFix.Fields.DropCopyOrder val = new QuickFix.Fields.DropCopyOrder();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.DropCopyOrder val) 
            { 
                this.DropCopyOrder = val;
            }
            
            public QuickFix.Fields.DropCopyOrder Get(QuickFix.Fields.DropCopyOrder val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.DropCopyOrder val) 
            { 
                return IsSetDropCopyOrder();
            }
            
            public bool IsSetDropCopyOrder() 
            { 
                return IsSetField(Tags.DropCopyOrder);
            }
            public QuickFix.Fields.OrderOrigination OrderOrigination
            { 
                get 
                {
                    QuickFix.Fields.OrderOrigination val = new QuickFix.Fields.OrderOrigination();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.OrderOrigination val) 
            { 
                this.OrderOrigination = val;
            }
            
            public QuickFix.Fields.OrderOrigination Get(QuickFix.Fields.OrderOrigination val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.OrderOrigination val) 
            { 
                return IsSetOrderOrigination();
            }
            
            public bool IsSetOrderOrigination() 
            { 
                return IsSetField(Tags.OrderOrigination);
            }
            public QuickFix.Fields.TrdRegPublicationReason TrdRegPublicationReason
            { 
                get 
                {
                    QuickFix.Fields.TrdRegPublicationReason val = new QuickFix.Fields.TrdRegPublicationReason();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TrdRegPublicationReason val) 
            { 
                this.TrdRegPublicationReason = val;
            }
            
            public QuickFix.Fields.TrdRegPublicationReason Get(QuickFix.Fields.TrdRegPublicationReason val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TrdRegPublicationReason val) 
            { 
                return IsSetTrdRegPublicationReason();
            }
            
            public bool IsSetTrdRegPublicationReason() 
            { 
                return IsSetField(Tags.TrdRegPublicationReason);
            }
            public QuickFix.Fields.TradingVenueRegulatoryTradeID TradingVenueRegulatoryTradeID
            { 
                get 
                {
                    QuickFix.Fields.TradingVenueRegulatoryTradeID val = new QuickFix.Fields.TradingVenueRegulatoryTradeID();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TradingVenueRegulatoryTradeID val) 
            { 
                this.TradingVenueRegulatoryTradeID = val;
            }
            
            public QuickFix.Fields.TradingVenueRegulatoryTradeID Get(QuickFix.Fields.TradingVenueRegulatoryTradeID val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TradingVenueRegulatoryTradeID val) 
            { 
                return IsSetTradingVenueRegulatoryTradeID();
            }
            
            public bool IsSetTradingVenueRegulatoryTradeID() 
            { 
                return IsSetField(Tags.TradingVenueRegulatoryTradeID);
            }
            public QuickFix.Fields.LastLiquidityIndicator LastLiquidityIndicator
            { 
                get 
                {
                    QuickFix.Fields.LastLiquidityIndicator val = new QuickFix.Fields.LastLiquidityIndicator();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LastLiquidityIndicator val) 
            { 
                this.LastLiquidityIndicator = val;
            }
            
            public QuickFix.Fields.LastLiquidityIndicator Get(QuickFix.Fields.LastLiquidityIndicator val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LastLiquidityIndicator val) 
            { 
                return IsSetLastLiquidityIndicator();
            }
            
            public bool IsSetLastLiquidityIndicator() 
            { 
                return IsSetField(Tags.LastLiquidityIndicator);
            }
            public QuickFix.Fields.NoOrderAttributes NoOrderAttributes
            { 
                get 
                {
                    QuickFix.Fields.NoOrderAttributes val = new QuickFix.Fields.NoOrderAttributes();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.NoOrderAttributes val) 
            { 
                this.NoOrderAttributes = val;
            }
            
            public QuickFix.Fields.NoOrderAttributes Get(QuickFix.Fields.NoOrderAttributes val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.NoOrderAttributes val) 
            { 
                return IsSetNoOrderAttributes();
            }
            
            public bool IsSetNoOrderAttributes() 
            { 
                return IsSetField(Tags.NoOrderAttributes);
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
            public class NoPartyIDsGroup : Group
            {
                public static int[] fieldOrder = {Tags.PartyID, Tags.PartyRole, Tags.PartyRoleQualifier, Tags.PartyIDSource, 0};
            
                public NoPartyIDsGroup() 
                  :base( Tags.NoPartyIDs, Tags.PartyID, fieldOrder)
                {
                }
            
                public override Group Clone()
                {
                    var clone = new NoPartyIDsGroup();
                    clone.CopyStateFrom(this);
                    return clone;
                }
            
                            public QuickFix.Fields.PartyID PartyID
                { 
                    get 
                    {
                        QuickFix.Fields.PartyID val = new QuickFix.Fields.PartyID();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.PartyID val) 
                { 
                    this.PartyID = val;
                }
                
                public QuickFix.Fields.PartyID Get(QuickFix.Fields.PartyID val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.PartyID val) 
                { 
                    return IsSetPartyID();
                }
                
                public bool IsSetPartyID() 
                { 
                    return IsSetField(Tags.PartyID);
                }
                public QuickFix.Fields.PartyRole PartyRole
                { 
                    get 
                    {
                        QuickFix.Fields.PartyRole val = new QuickFix.Fields.PartyRole();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.PartyRole val) 
                { 
                    this.PartyRole = val;
                }
                
                public QuickFix.Fields.PartyRole Get(QuickFix.Fields.PartyRole val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.PartyRole val) 
                { 
                    return IsSetPartyRole();
                }
                
                public bool IsSetPartyRole() 
                { 
                    return IsSetField(Tags.PartyRole);
                }
                public QuickFix.Fields.PartyRoleQualifier PartyRoleQualifier
                { 
                    get 
                    {
                        QuickFix.Fields.PartyRoleQualifier val = new QuickFix.Fields.PartyRoleQualifier();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.PartyRoleQualifier val) 
                { 
                    this.PartyRoleQualifier = val;
                }
                
                public QuickFix.Fields.PartyRoleQualifier Get(QuickFix.Fields.PartyRoleQualifier val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.PartyRoleQualifier val) 
                { 
                    return IsSetPartyRoleQualifier();
                }
                
                public bool IsSetPartyRoleQualifier() 
                { 
                    return IsSetField(Tags.PartyRoleQualifier);
                }
                public QuickFix.Fields.PartyIDSource PartyIDSource
                { 
                    get 
                    {
                        QuickFix.Fields.PartyIDSource val = new QuickFix.Fields.PartyIDSource();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.PartyIDSource val) 
                { 
                    this.PartyIDSource = val;
                }
                
                public QuickFix.Fields.PartyIDSource Get(QuickFix.Fields.PartyIDSource val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.PartyIDSource val) 
                { 
                    return IsSetPartyIDSource();
                }
                
                public bool IsSetPartyIDSource() 
                { 
                    return IsSetField(Tags.PartyIDSource);
                }
            
            }
            public class NoSecurityAltIDGroup : Group
            {
                public static int[] fieldOrder = {Tags.SecurityAltID, Tags.SecurityAltIDSource, 0};
            
                public NoSecurityAltIDGroup() 
                  :base( Tags.NoSecurityAltID, Tags.SecurityAltID, fieldOrder)
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
            public class NoLegsGroup : Group
            {
                public static int[] fieldOrder = {Tags.LegSymbol, Tags.LegSecurityID, Tags.LegSecurityType, Tags.LegMaturityMonthYear, Tags.LegMaturityDate, Tags.LegMaturityDay, Tags.LegStrikePrice, Tags.LegPutOrCall, Tags.LegOptAttribute, Tags.LegSecurityExchange, Tags.LegExDestination, Tags.LegSide, Tags.LegRatioQty, Tags.LegCurrency, Tags.LegPrice, Tags.LegDeliveryTerm, Tags.LegDeliveryDate, Tags.LegContractYearMonth, Tags.NoLegSecurityAltID, 0};
            
                public NoLegsGroup() 
                  :base( Tags.NoLegs, Tags.LegSymbol, fieldOrder)
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
                public QuickFix.Fields.LegContractYearMonth LegContractYearMonth
                { 
                    get 
                    {
                        QuickFix.Fields.LegContractYearMonth val = new QuickFix.Fields.LegContractYearMonth();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.LegContractYearMonth val) 
                { 
                    this.LegContractYearMonth = val;
                }
                
                public QuickFix.Fields.LegContractYearMonth Get(QuickFix.Fields.LegContractYearMonth val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.LegContractYearMonth val) 
                { 
                    return IsSetLegContractYearMonth();
                }
                
                public bool IsSetLegContractYearMonth() 
                { 
                    return IsSetField(Tags.LegContractYearMonth);
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
                    public static int[] fieldOrder = {Tags.LegSecurityAltID, Tags.LegSecurityAltIDSource, 0};
                
                    public NoLegSecurityAltIDGroup() 
                      :base( Tags.NoLegSecurityAltID, Tags.LegSecurityAltID, fieldOrder)
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
            public class NoLinksGroup : Group
            {
                public static int[] fieldOrder = {Tags.LinkID, Tags.LinkType, 0};
            
                public NoLinksGroup() 
                  :base( Tags.NoLinks, Tags.LinkID, fieldOrder)
                {
                }
            
                public override Group Clone()
                {
                    var clone = new NoLinksGroup();
                    clone.CopyStateFrom(this);
                    return clone;
                }
            
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
            public class NoFillsGroup : Group
            {
                public static int[] fieldOrder = {Tags.FillExecID, Tags.FillPx, Tags.FillQty, 0};
            
                public NoFillsGroup() 
                  :base( Tags.NoFills, Tags.FillExecID, fieldOrder)
                {
                }
            
                public override Group Clone()
                {
                    var clone = new NoFillsGroup();
                    clone.CopyStateFrom(this);
                    return clone;
                }
            
                            public QuickFix.Fields.FillExecID FillExecID
                { 
                    get 
                    {
                        QuickFix.Fields.FillExecID val = new QuickFix.Fields.FillExecID();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.FillExecID val) 
                { 
                    this.FillExecID = val;
                }
                
                public QuickFix.Fields.FillExecID Get(QuickFix.Fields.FillExecID val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.FillExecID val) 
                { 
                    return IsSetFillExecID();
                }
                
                public bool IsSetFillExecID() 
                { 
                    return IsSetField(Tags.FillExecID);
                }
                public QuickFix.Fields.FillPx FillPx
                { 
                    get 
                    {
                        QuickFix.Fields.FillPx val = new QuickFix.Fields.FillPx();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.FillPx val) 
                { 
                    this.FillPx = val;
                }
                
                public QuickFix.Fields.FillPx Get(QuickFix.Fields.FillPx val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.FillPx val) 
                { 
                    return IsSetFillPx();
                }
                
                public bool IsSetFillPx() 
                { 
                    return IsSetField(Tags.FillPx);
                }
                public QuickFix.Fields.FillQty FillQty
                { 
                    get 
                    {
                        QuickFix.Fields.FillQty val = new QuickFix.Fields.FillQty();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.FillQty val) 
                { 
                    this.FillQty = val;
                }
                
                public QuickFix.Fields.FillQty Get(QuickFix.Fields.FillQty val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.FillQty val) 
                { 
                    return IsSetFillQty();
                }
                
                public bool IsSetFillQty() 
                { 
                    return IsSetField(Tags.FillQty);
                }
            
            }
            public class NoOrderAttributesGroup : Group
            {
                public static int[] fieldOrder = {Tags.OrderAttributeType, Tags.OrderAttributeValue, 0};
            
                public NoOrderAttributesGroup() 
                  :base( Tags.NoOrderAttributes, Tags.OrderAttributeType, fieldOrder)
                {
                }
            
                public override Group Clone()
                {
                    var clone = new NoOrderAttributesGroup();
                    clone.CopyStateFrom(this);
                    return clone;
                }
            
                            public QuickFix.Fields.OrderAttributeType OrderAttributeType
                { 
                    get 
                    {
                        QuickFix.Fields.OrderAttributeType val = new QuickFix.Fields.OrderAttributeType();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.OrderAttributeType val) 
                { 
                    this.OrderAttributeType = val;
                }
                
                public QuickFix.Fields.OrderAttributeType Get(QuickFix.Fields.OrderAttributeType val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.OrderAttributeType val) 
                { 
                    return IsSetOrderAttributeType();
                }
                
                public bool IsSetOrderAttributeType() 
                { 
                    return IsSetField(Tags.OrderAttributeType);
                }
                public QuickFix.Fields.OrderAttributeValue OrderAttributeValue
                { 
                    get 
                    {
                        QuickFix.Fields.OrderAttributeValue val = new QuickFix.Fields.OrderAttributeValue();
                        GetField(val);
                        return val;
                    }
                    set { SetField(value); }
                }
                
                public void Set(QuickFix.Fields.OrderAttributeValue val) 
                { 
                    this.OrderAttributeValue = val;
                }
                
                public QuickFix.Fields.OrderAttributeValue Get(QuickFix.Fields.OrderAttributeValue val) 
                { 
                    GetField(val);
                    return val;
                }
                
                public bool IsSet(QuickFix.Fields.OrderAttributeValue val) 
                { 
                    return IsSetOrderAttributeValue();
                }
                
                public bool IsSetOrderAttributeValue() 
                { 
                    return IsSetField(Tags.OrderAttributeValue);
                }
            
            }
        }
    }
}
