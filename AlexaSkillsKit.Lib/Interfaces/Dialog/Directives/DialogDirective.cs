// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Interfaces.Dialog.Directives
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