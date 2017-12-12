namespace AlexaSkillsKit.Interfaces.Dialog.Directives
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/dialog-interface-reference.html#confirmslot
    /// </summary>
    public class DialogConfirmSlotDirective : DialogDirective
    {
        public DialogConfirmSlotDirective() : base("ConfirmSlot") {

        }

        public virtual string SlotToConfirm {
            get;
            set;
        }
    }
}