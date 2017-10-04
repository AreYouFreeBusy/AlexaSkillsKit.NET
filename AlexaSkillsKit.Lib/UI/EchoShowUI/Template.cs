using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.UI.EchoShowUI
{
    public class Template
    {
        private const string BACK_BUTTON_STATE = "HIDDEN";
        private const string TEMPLATE_TYPE = "BodyTemplate2";
        private const string TOKEN_VALUE = "";

        public virtual string Type { get; set; }
        public virtual string Title { get; set; }
        public virtual string Token { get; set; }
        public virtual TextContent TextContent { get; set; }
        public virtual Image Image { get; set; }
        public virtual string BackButton { get; set; }
        public virtual Image BackgroundImage { get; set; }

        public Template(string backButton = BACK_BUTTON_STATE, string type = TEMPLATE_TYPE, string token = TOKEN_VALUE)
        {
            this.BackButton = backButton;
            this.Type = type;
            this.Token = token;
        }
    }
}
