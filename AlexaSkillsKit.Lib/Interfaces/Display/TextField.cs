namespace AlexaSkillsKit.Interfaces.Display
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#textcontent-object-specifications
    /// </summary>
    public class TextField
    {
        public virtual TextTypeEnum Type {
            get;
            set;
        }

        public virtual string Text {
            get;
            set;
        }

        public enum TextTypeEnum
        {
            PlainText,
            RichText
        }
    }
}