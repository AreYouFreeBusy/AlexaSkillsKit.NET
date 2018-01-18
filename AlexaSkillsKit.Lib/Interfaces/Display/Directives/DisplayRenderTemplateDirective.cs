// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Interfaces.Display.Directives
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#form-of-the-displayrendertemplate-directive
    /// </summary>
    public class DisplayRenderTemplateDirective : Directive
    {
        public DisplayRenderTemplateDirective() : base("Display.RenderTemplate") {

        }

        public virtual DisplayTemplate Template {
            get;
            set;
        }
    }
}