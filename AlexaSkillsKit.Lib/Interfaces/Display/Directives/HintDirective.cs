// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

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