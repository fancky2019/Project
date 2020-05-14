// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class UnknownFixMessage : Message
        {
            public const string MsgType = "u";

            public UnknownFixMessage() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("u"));
            }




        }
    }
}
