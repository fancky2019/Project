// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class OrderCancelReplaceRequest : Message
        {
            public const string MsgType = "G";

            public OrderCancelReplaceRequest() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("G"));
            }

            public OrderCancelReplaceRequest(
                    QuickFix.Fields.ClOrdID aClOrdID,
                    QuickFix.Fields.Account aAccount,
                    QuickFix.Fields.OrderQty aOrderQty,
                    QuickFix.Fields.Side aSide,
                    QuickFix.Fields.OrdType aOrdType
                ) : this()
            {
                this.ClOrdID = aClOrdID;
                this.Account = aAccount;
                this.OrderQty = aOrderQty;
                this.Side = aSide;
                this.OrdType = aOrdType;
            }

            //fancky  add

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
            public QuickFix.Fields.TextA TextA
            { 
                get 
                {
                    QuickFix.Fields.TextA val = new QuickFix.Fields.TextA();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TextA val) 
            { 
                this.TextA = val;
            }
            
            public QuickFix.Fields.TextA Get(QuickFix.Fields.TextA val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TextA val) 
            { 
                return IsSetTextA();
            }
            
            public bool IsSetTextA() 
            { 
                return IsSetField(Tags.TextA);
            }
            public QuickFix.Fields.TextB TextB
            { 
                get 
                {
                    QuickFix.Fields.TextB val = new QuickFix.Fields.TextB();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TextB val) 
            { 
                this.TextB = val;
            }
            
            public QuickFix.Fields.TextB Get(QuickFix.Fields.TextB val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TextB val) 
            { 
                return IsSetTextB();
            }
            
            public bool IsSetTextB() 
            { 
                return IsSetField(Tags.TextB);
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
            public QuickFix.Fields.TriggerQtyType TriggerQtyType
            { 
                get 
                {
                    QuickFix.Fields.TriggerQtyType val = new QuickFix.Fields.TriggerQtyType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TriggerQtyType val) 
            { 
                this.TriggerQtyType = val;
            }
            
            public QuickFix.Fields.TriggerQtyType Get(QuickFix.Fields.TriggerQtyType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TriggerQtyType val) 
            { 
                return IsSetTriggerQtyType();
            }
            
            public bool IsSetTriggerQtyType() 
            { 
                return IsSetField(Tags.TriggerQtyType);
            }
            public QuickFix.Fields.TriggerQtyCompare TriggerQtyCompare
            { 
                get 
                {
                    QuickFix.Fields.TriggerQtyCompare val = new QuickFix.Fields.TriggerQtyCompare();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TriggerQtyCompare val) 
            { 
                this.TriggerQtyCompare = val;
            }
            
            public QuickFix.Fields.TriggerQtyCompare Get(QuickFix.Fields.TriggerQtyCompare val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TriggerQtyCompare val) 
            { 
                return IsSetTriggerQtyCompare();
            }
            
            public bool IsSetTriggerQtyCompare() 
            { 
                return IsSetField(Tags.TriggerQtyCompare);
            }
            public QuickFix.Fields.TriggerQty TriggerQty
            { 
                get 
                {
                    QuickFix.Fields.TriggerQty val = new QuickFix.Fields.TriggerQty();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TriggerQty val) 
            { 
                this.TriggerQty = val;
            }
            
            public QuickFix.Fields.TriggerQty Get(QuickFix.Fields.TriggerQty val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TriggerQty val) 
            { 
                return IsSetTriggerQty();
            }
            
            public bool IsSetTriggerQty() 
            { 
                return IsSetField(Tags.TriggerQty);
            }
            public QuickFix.Fields.TriggerLTPReset TriggerLTPReset
            { 
                get 
                {
                    QuickFix.Fields.TriggerLTPReset val = new QuickFix.Fields.TriggerLTPReset();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TriggerLTPReset val) 
            { 
                this.TriggerLTPReset = val;
            }
            
            public QuickFix.Fields.TriggerLTPReset Get(QuickFix.Fields.TriggerLTPReset val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TriggerLTPReset val) 
            { 
                return IsSetTriggerLTPReset();
            }
            
            public bool IsSetTriggerLTPReset() 
            { 
                return IsSetField(Tags.TriggerLTPReset);
            }
            public QuickFix.Fields.TTStopLimitPriceType TTStopLimitPriceType
            { 
                get 
                {
                    QuickFix.Fields.TTStopLimitPriceType val = new QuickFix.Fields.TTStopLimitPriceType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopLimitPriceType val) 
            { 
                this.TTStopLimitPriceType = val;
            }
            
            public QuickFix.Fields.TTStopLimitPriceType Get(QuickFix.Fields.TTStopLimitPriceType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopLimitPriceType val) 
            { 
                return IsSetTTStopLimitPriceType();
            }
            
            public bool IsSetTTStopLimitPriceType() 
            { 
                return IsSetField(Tags.TTStopLimitPriceType);
            }
            public QuickFix.Fields.TTStopWithATickType TTStopWithATickType
            { 
                get 
                {
                    QuickFix.Fields.TTStopWithATickType val = new QuickFix.Fields.TTStopWithATickType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopWithATickType val) 
            { 
                this.TTStopWithATickType = val;
            }
            
            public QuickFix.Fields.TTStopWithATickType Get(QuickFix.Fields.TTStopWithATickType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopWithATickType val) 
            { 
                return IsSetTTStopWithATickType();
            }
            
            public bool IsSetTTStopWithATickType() 
            { 
                return IsSetField(Tags.TTStopWithATickType);
            }
            public QuickFix.Fields.TTStopWithATick TTStopWithATick
            { 
                get 
                {
                    QuickFix.Fields.TTStopWithATick val = new QuickFix.Fields.TTStopWithATick();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopWithATick val) 
            { 
                this.TTStopWithATick = val;
            }
            
            public QuickFix.Fields.TTStopWithATick Get(QuickFix.Fields.TTStopWithATick val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopWithATick val) 
            { 
                return IsSetTTStopWithATick();
            }
            
            public bool IsSetTTStopWithATick() 
            { 
                return IsSetField(Tags.TTStopWithATick);
            }
            public QuickFix.Fields.Payup Payup
            { 
                get 
                {
                    QuickFix.Fields.Payup val = new QuickFix.Fields.Payup();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.Payup val) 
            { 
                this.Payup = val;
            }
            
            public QuickFix.Fields.Payup Get(QuickFix.Fields.Payup val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.Payup val) 
            { 
                return IsSetPayup();
            }
            
            public bool IsSetPayup() 
            { 
                return IsSetField(Tags.Payup);
            }
            public QuickFix.Fields.TTStopTriggerPriceType TTStopTriggerPriceType
            { 
                get 
                {
                    QuickFix.Fields.TTStopTriggerPriceType val = new QuickFix.Fields.TTStopTriggerPriceType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopTriggerPriceType val) 
            { 
                this.TTStopTriggerPriceType = val;
            }
            
            public QuickFix.Fields.TTStopTriggerPriceType Get(QuickFix.Fields.TTStopTriggerPriceType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopTriggerPriceType val) 
            { 
                return IsSetTTStopTriggerPriceType();
            }
            
            public bool IsSetTTStopTriggerPriceType() 
            { 
                return IsSetField(Tags.TTStopTriggerPriceType);
            }
            public QuickFix.Fields.TTStopIsTrlTrg TTStopIsTrlTrg
            { 
                get 
                {
                    QuickFix.Fields.TTStopIsTrlTrg val = new QuickFix.Fields.TTStopIsTrlTrg();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopIsTrlTrg val) 
            { 
                this.TTStopIsTrlTrg = val;
            }
            
            public QuickFix.Fields.TTStopIsTrlTrg Get(QuickFix.Fields.TTStopIsTrlTrg val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopIsTrlTrg val) 
            { 
                return IsSetTTStopIsTrlTrg();
            }
            
            public bool IsSetTTStopIsTrlTrg() 
            { 
                return IsSetField(Tags.TTStopIsTrlTrg);
            }
            public QuickFix.Fields.TTStopTriggerTicksAway TTStopTriggerTicksAway
            { 
                get 
                {
                    QuickFix.Fields.TTStopTriggerTicksAway val = new QuickFix.Fields.TTStopTriggerTicksAway();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopTriggerTicksAway val) 
            { 
                this.TTStopTriggerTicksAway = val;
            }
            
            public QuickFix.Fields.TTStopTriggerTicksAway Get(QuickFix.Fields.TTStopTriggerTicksAway val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopTriggerTicksAway val) 
            { 
                return IsSetTTStopTriggerTicksAway();
            }
            
            public bool IsSetTTStopTriggerTicksAway() 
            { 
                return IsSetField(Tags.TTStopTriggerTicksAway);
            }
            public QuickFix.Fields.TTStopTriggerQtyType TTStopTriggerQtyType
            { 
                get 
                {
                    QuickFix.Fields.TTStopTriggerQtyType val = new QuickFix.Fields.TTStopTriggerQtyType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopTriggerQtyType val) 
            { 
                this.TTStopTriggerQtyType = val;
            }
            
            public QuickFix.Fields.TTStopTriggerQtyType Get(QuickFix.Fields.TTStopTriggerQtyType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopTriggerQtyType val) 
            { 
                return IsSetTTStopTriggerQtyType();
            }
            
            public bool IsSetTTStopTriggerQtyType() 
            { 
                return IsSetField(Tags.TTStopTriggerQtyType);
            }
            public QuickFix.Fields.TTStopTriggerQTyCompare TTStopTriggerQTyCompare
            { 
                get 
                {
                    QuickFix.Fields.TTStopTriggerQTyCompare val = new QuickFix.Fields.TTStopTriggerQTyCompare();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopTriggerQTyCompare val) 
            { 
                this.TTStopTriggerQTyCompare = val;
            }
            
            public QuickFix.Fields.TTStopTriggerQTyCompare Get(QuickFix.Fields.TTStopTriggerQTyCompare val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopTriggerQTyCompare val) 
            { 
                return IsSetTTStopTriggerQTyCompare();
            }
            
            public bool IsSetTTStopTriggerQTyCompare() 
            { 
                return IsSetField(Tags.TTStopTriggerQTyCompare);
            }
            public QuickFix.Fields.TTStopTriggerQty TTStopTriggerQty
            { 
                get 
                {
                    QuickFix.Fields.TTStopTriggerQty val = new QuickFix.Fields.TTStopTriggerQty();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopTriggerQty val) 
            { 
                this.TTStopTriggerQty = val;
            }
            
            public QuickFix.Fields.TTStopTriggerQty Get(QuickFix.Fields.TTStopTriggerQty val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopTriggerQty val) 
            { 
                return IsSetTTStopTriggerQty();
            }
            
            public bool IsSetTTStopTriggerQty() 
            { 
                return IsSetField(Tags.TTStopTriggerQty);
            }
            public QuickFix.Fields.TTStopTriggerLTPReset TTStopTriggerLTPReset
            { 
                get 
                {
                    QuickFix.Fields.TTStopTriggerLTPReset val = new QuickFix.Fields.TTStopTriggerLTPReset();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopTriggerLTPReset val) 
            { 
                this.TTStopTriggerLTPReset = val;
            }
            
            public QuickFix.Fields.TTStopTriggerLTPReset Get(QuickFix.Fields.TTStopTriggerLTPReset val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopTriggerLTPReset val) 
            { 
                return IsSetTTStopTriggerLTPReset();
            }
            
            public bool IsSetTTStopTriggerLTPReset() 
            { 
                return IsSetField(Tags.TTStopTriggerLTPReset);
            }
            public QuickFix.Fields.TTStopTriggeredOrderType TTStopTriggeredOrderType
            { 
                get 
                {
                    QuickFix.Fields.TTStopTriggeredOrderType val = new QuickFix.Fields.TTStopTriggeredOrderType();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopTriggeredOrderType val) 
            { 
                this.TTStopTriggeredOrderType = val;
            }
            
            public QuickFix.Fields.TTStopTriggeredOrderType Get(QuickFix.Fields.TTStopTriggeredOrderType val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopTriggeredOrderType val) 
            { 
                return IsSetTTStopTriggeredOrderType();
            }
            
            public bool IsSetTTStopTriggeredOrderType() 
            { 
                return IsSetField(Tags.TTStopTriggeredOrderType);
            }
            public QuickFix.Fields.TTStopTriggeredOrderPrice TTStopTriggeredOrderPrice
            { 
                get 
                {
                    QuickFix.Fields.TTStopTriggeredOrderPrice val = new QuickFix.Fields.TTStopTriggeredOrderPrice();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopTriggeredOrderPrice val) 
            { 
                this.TTStopTriggeredOrderPrice = val;
            }
            
            public QuickFix.Fields.TTStopTriggeredOrderPrice Get(QuickFix.Fields.TTStopTriggeredOrderPrice val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopTriggeredOrderPrice val) 
            { 
                return IsSetTTStopTriggeredOrderPrice();
            }
            
            public bool IsSetTTStopTriggeredOrderPrice() 
            { 
                return IsSetField(Tags.TTStopTriggeredOrderPrice);
            }
            public QuickFix.Fields.TTStopLimitTicksAway TTStopLimitTicksAway
            { 
                get 
                {
                    QuickFix.Fields.TTStopLimitTicksAway val = new QuickFix.Fields.TTStopLimitTicksAway();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopLimitTicksAway val) 
            { 
                this.TTStopLimitTicksAway = val;
            }
            
            public QuickFix.Fields.TTStopLimitTicksAway Get(QuickFix.Fields.TTStopLimitTicksAway val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopLimitTicksAway val) 
            { 
                return IsSetTTStopLimitTicksAway();
            }
            
            public bool IsSetTTStopLimitTicksAway() 
            { 
                return IsSetField(Tags.TTStopLimitTicksAway);
            }
            public QuickFix.Fields.TTStopPayup TTStopPayup
            { 
                get 
                {
                    QuickFix.Fields.TTStopPayup val = new QuickFix.Fields.TTStopPayup();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.TTStopPayup val) 
            { 
                this.TTStopPayup = val;
            }
            
            public QuickFix.Fields.TTStopPayup Get(QuickFix.Fields.TTStopPayup val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.TTStopPayup val) 
            { 
                return IsSetTTStopPayup();
            }
            
            public bool IsSetTTStopPayup() 
            { 
                return IsSetField(Tags.TTStopPayup);
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
