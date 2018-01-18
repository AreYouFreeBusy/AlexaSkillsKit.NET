// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

namespace AlexaSkillsKit.Interfaces.Dialog.Directives
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/dialog-interface-reference.html#elicitslot
    /// </summary>
    public class DialogElicitSlotDirective : DialogDirective
    {
        public DialogElicitSlotDirective() : base("ElicitSlot") {

        }

        public virtual string SlotToElicit {
            get;
            set;
        }
    }
}