using System.Collections.Generic;

namespace AlexaSkillsKit.Directives.Display
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#display-template-elements
    /// </summary>
    public class DisplayTemplate
    {
        public virtual string Type {
            get;
            set;
        }

        public virtual string Token {
            get;
            set;
        }

        public virtual string Title {
            get;
            set;
        }

        public virtual TextContent TextContent {
            get;
            set;
        }

        public virtual ButtonStateEnum BackButton {
            get;
            set;
        }

        public virtual Image BackgroundImage {
            get;
            set;
        }

        public virtual Image Image {
            get;
            set;
        }

        public virtual IEnumerable<ListItem> ListItems {
            get;
            set;
        }

        public enum ButtonStateEnum
        {
            HIDDEN,
            VISIBLE
        }
    }
}