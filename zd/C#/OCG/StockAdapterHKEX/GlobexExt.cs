using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFix.Fields;

namespace StockAdapterHKEX
{
    
    public class GlobexExt
    {
        
    }

    public class MyExtTags
    {
        public const int DisplayFactor = 9787;
    }

    public sealed class DisplayFactor : DecimalField
    {
        public DisplayFactor()
            : base(MyExtTags.DisplayFactor) { }

        public DisplayFactor(decimal val)
            : base(MyExtTags.DisplayFactor, val) { }
    }

    
    /*
    public sealed class CtiCode : CharField   
    {
        public CtiCode() : base(CMETags.CtiCode) { }
        public CtiCode(char val) : base(CMETags.CtiCode, val) { }   
        // Field Enumerations        
        public const char BUY = '1';       
        public const char SELL = '2';      
        public const char TRADE = '3';    
        public const char CROSS = '4';
    }
    */
    /*
    public sealed class DeliverToCompID : StringField
    {
        public DeliverToCompID()
            : base(Tags.DeliverToCompID) { }
        public DeliverToCompID(string val)
            : base(Tags.DeliverToCompID, val) { }

    }
    */
}
