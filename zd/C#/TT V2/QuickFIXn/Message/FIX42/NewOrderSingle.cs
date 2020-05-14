// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42
    {
        public class NewOrderSingle : Message
        {
            public const string MsgType = "D";

            public NewOrderSingle() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("D"));
            }

            public NewOrderSingle(
 QuickFix.Fields.ClOrdID aClOrdID,
                    QuickFix.Fields.SecurityExchange aSecurityExchange,
                    QuickFix.Fields.Account aAccount,
                    QuickFix.Fields.OrderQty aOrderQty,
                    QuickFix.Fields.Side aSide,
                    QuickFix.Fields.OrdType aOrdType
                ) : this()
            {
                this.ClOrdID = aClOrdID;
                this.SecurityExchange = aSecurityExchange;
                this.Account = aAccount;
                this.OrderQty = aOrderQty;
                this.Side = aSide;
                this.OrdType = aOrdType;
            }



            #region   //fancky add
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


            public QuickFix.Fields.CheckSum CheckSum
            {
                get
                {
                    QuickFix.Fields.CheckSum val = new QuickFix.Fields.CheckSum();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }

            public void Set(QuickFix.Fields.CheckSum val)
            {
                this.CheckSum = val;
            }

            public QuickFix.Fields.CheckSum Get(QuickFix.Fields.CheckSum val)
            {
                GetField(val);
                return val;
            }

            public bool IsSet(QuickFix.Fields.CheckSum val)
            {
                return IsSetCheckSum();
            }

            public bool IsSetCheckSum()
            {
                return IsSetField(Tags.CheckSum);
            }


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




            //public QuickFix.Fields.PartyID PartyID
            //{
            //    get
            //    {
            //        QuickFix.Fields.PartyID val = new QuickFix.Fields.PartyID();
            //        GetField(val);
            //        return val;
            //    }
            //    set { SetField(value); }
            //}

            //public void Set(QuickFix.Fields.PartyID val)
            //{
            //    this.PartyID = val;
            //}

            //public QuickFix.Fields.PartyID Get(QuickFix.Fields.PartyID val)
            //{
            //    GetField(val);
            //    return val;
            //}

            //public bool IsSet(QuickFix.Fields.PartyID val)
            //{
            //    return IsSetPartyID();
            //}

            //public bool IsSetPartyID()
            //{
            //    return IsSetField(Tags.PartyID);
            //}

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

            #endregion





















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
            public class NoPartyIDsGroup : Group
            {
                public static int[] fieldOrder = { Tags.PartyID, Tags.PartyRole, Tags.PartyRoleQualifier, Tags.PartyIDSource, 0 };

                public NoPartyIDsGroup()
                  : base(Tags.NoPartyIDs, Tags.PartyID, fieldOrder)
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
            public class NoStrategyParametersGroup : Group
            {
                public static int[] fieldOrder = { Tags.StrategyParameterName, Tags.StrategyParameterType, Tags.StrategyParameterValue, 0 };

                public NoStrategyParametersGroup()
                  : base(Tags.NoStrategyParameters, Tags.StrategyParameterName, fieldOrder)
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
                public static int[] fieldOrder = { Tags.LegSymbol, Tags.LegSecurityID, Tags.LegSecurityType, Tags.LegMaturityMonthYear, Tags.LegMaturityDate, Tags.LegMaturityDay, Tags.LegStrikePrice, Tags.LegPutOrCall, Tags.LegOptAttribute, Tags.LegSecurityExchange, Tags.LegExDestination, Tags.LegSide, Tags.LegRatioQty, Tags.LegCurrency, Tags.LegPrice, Tags.LegDeliveryTerm, Tags.LegDeliveryDate, Tags.LegContractYearMonth, Tags.NoLegSecurityAltID, 0 };

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
            public class NoOrderAttributesGroup : Group
            {
                public static int[] fieldOrder = { Tags.OrderAttributeType, Tags.OrderAttributeValue, 0 };

                public NoOrderAttributesGroup()
                  : base(Tags.NoOrderAttributes, Tags.OrderAttributeType, fieldOrder)
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
