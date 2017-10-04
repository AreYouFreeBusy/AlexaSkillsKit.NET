using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.UI.EchoShowUI
{
    class MainContentText
    {
        private const string TEXT_TYPE = "RichText";

        public virtual string Type { get; set; }
        public virtual string Text { get; set; }
        public MainContentText(string type = TEXT_TYPE)
        {
            this.Type = type;
        }
    }
}
