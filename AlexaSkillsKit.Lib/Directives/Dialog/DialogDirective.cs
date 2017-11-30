using AlexaSkillsKit.Slu;

namespace AlexaSkillsKit.Directives.Dialog
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/dialog-interface-reference.html#directives
    /// </summary>
    public class DialogDirective: Directive
    {
        public DialogDirective(string type) : base(type) {

        }

        public virtual Intent UpdatedIntent {
            get;
            set;
        }
    }
}