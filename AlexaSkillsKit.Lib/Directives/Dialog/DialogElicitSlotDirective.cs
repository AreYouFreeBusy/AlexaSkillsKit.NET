using AlexaSkillsKit.Slu;

namespace AlexaSkillsKit.Directives.Dialog
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/dialog-interface-reference.html#elicitslot
    /// </summary>
    public class DialogElicitSlotDirective : DialogDirective
    {
        public DialogElicitSlotDirective() : base("Dialog.ElicitSlot") {

        }

        public virtual string SlotToElicit {
            get;
            set;
        }
    }
}