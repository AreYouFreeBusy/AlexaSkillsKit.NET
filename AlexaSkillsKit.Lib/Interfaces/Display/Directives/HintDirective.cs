using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Interfaces.Display.Directives
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#hint-directive
    /// </summary>
    public class HintDirective : Directive
    {
        public HintDirective() : base("Hint") {

        }

        public virtual TextField Hint {
            get;
            set;
        }
    }
}