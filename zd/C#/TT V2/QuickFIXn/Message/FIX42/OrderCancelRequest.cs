// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class OrderCancelRequest : Message
        {
            public const string MsgType = "F";

            public OrderCancelRequest() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("F"));
            }

            public OrderCancelRequest(
                    QuickFix.Fields.ClOrdID aClOrdID
                ) : this()
            {
                this.ClOrdID = aClOrdID;
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



            #region   fancky add
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

            #endregion
        }
    }
}
