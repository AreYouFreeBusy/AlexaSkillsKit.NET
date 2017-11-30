using System.Collections.Generic;

namespace AlexaSkillsKit.Directives.Display
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#image-object-specifications
    /// </summary>
    public class Image
    {
        public virtual string ContentDescription {
            get;
            set;
        }

        public virtual IEnumerable<ImageSource> Sources {
            get;
            set;
        }
    }
}