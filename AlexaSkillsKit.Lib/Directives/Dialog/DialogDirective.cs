using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Directives.Dialog
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/dialog-interface-reference.html#directives
    /// </summary>
    public class DialogDirective: Directive
    {
        public DialogDirective(string subtype) : base($"Dialog.{subtype}") {

        }

        public virtual Intent UpdatedIntent {
            get;
            set;
        }
    }
}