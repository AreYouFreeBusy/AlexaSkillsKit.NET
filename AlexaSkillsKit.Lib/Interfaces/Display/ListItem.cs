namespace AlexaSkillsKit.Interfaces.Display
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#display-template-elements
    /// </summary>
    public class ListItem
    {
        public virtual string Token {
            get;
            set;
        }

        public virtual DisplayImage Image {
            get;
            set;
        }

        public virtual TextContent TextContent {
            get;
            set;
        }
    }
}