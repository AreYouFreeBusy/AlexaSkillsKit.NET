using System.Collections.Generic;

namespace AlexaSkillsKit.Interfaces.Display
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#image-object-specifications
    /// </summary>
    public class DisplayImage
    {
        public virtual string ContentDescription {
            get;
            set;
        }

        public virtual IEnumerable<DisplayImageSource> Sources {
            get;
            set;
        }
    }
}