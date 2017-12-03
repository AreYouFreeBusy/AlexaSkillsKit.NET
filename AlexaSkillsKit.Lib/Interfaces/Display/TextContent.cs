namespace AlexaSkillsKit.Interfaces.Display
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#textcontent-object-specifications
    /// </summary>
    public class TextContent
    {
        public virtual TextField PrimaryText {
            get;
            set;
        }

        public virtual TextField SecondaryText {
            get;
            set;
        }

        public virtual TextField TertiaryText {
            get;
            set;
        }
    }
}