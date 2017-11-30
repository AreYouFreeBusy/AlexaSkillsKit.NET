using AlexaSkillsKit.Slu;

namespace AlexaSkillsKit.Directives.Dialog
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/dialog-interface-reference.html#confirmslot
    /// </summary>
    public class DialogConfirmSlotDirective : DialogDirective
    {
        public DialogConfirmSlotDirective() : base("Dialog.ConfirmSlot") {

        }

        public virtual Slot SlotToConfirm {
            get;
            set;
        }
    }
}