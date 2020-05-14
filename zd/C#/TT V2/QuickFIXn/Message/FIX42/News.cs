// This is a generated file.  Don't edit it directly!

using QuickFix.Fields;
namespace QuickFix
{
    namespace FIX42 
    {
        public class News : Message
        {
            public const string MsgType = "B";

            public News() : base()
            {
                this.Header.SetField(new QuickFix.Fields.MsgType("B"));
            }

            public News(
                    QuickFix.Fields.Headline aHeadline,
                    QuickFix.Fields.LinesOfText aLinesOfText,
                    QuickFix.Fields.Text aText
                ) : this()
            {
                this.Headline = aHeadline;
                this.LinesOfText = aLinesOfText;
                this.Text = aText;
            }

            public QuickFix.Fields.Headline Headline
            { 
                get 
                {
                    QuickFix.Fields.Headline val = new QuickFix.Fields.Headline();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.Headline val) 
            { 
                this.Headline = val;
            }
            
            public QuickFix.Fields.Headline Get(QuickFix.Fields.Headline val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.Headline val) 
            { 
                return IsSetHeadline();
            }
            
            public bool IsSetHeadline() 
            { 
                return IsSetField(Tags.Headline);
            }
            public QuickFix.Fields.LinesOfText LinesOfText
            { 
                get 
                {
                    QuickFix.Fields.LinesOfText val = new QuickFix.Fields.LinesOfText();
                    GetField(val);
                    return val;
                }
                set { SetField(value); }
            }
            
            public void Set(QuickFix.Fields.LinesOfText val) 
            { 
                this.LinesOfText = val;
            }
            
            public QuickFix.Fields.LinesOfText Get(QuickFix.Fields.LinesOfText val) 
            { 
                GetField(val);
                return val;
            }
            
            public bool IsSet(QuickFix.Fields.LinesOfText val) 
            { 
                return IsSetLinesOfText();
            }
            
            public bool IsSetLinesOfText() 
            { 
                return IsSetField(Tags.LinesOfText);
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

        }
    }
}
