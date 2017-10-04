using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.UI.EchoShowUI
{
    public class Directive
    {
        private const string DISPLAY_TYPE = "Display.RenderTemplate";

        public virtual string Type { get; set; }
        public virtual Template Template { get; set; }

        public Directive(string type = DISPLAY_TYPE)
        {
            this.Type = type;
        }
    }
}
