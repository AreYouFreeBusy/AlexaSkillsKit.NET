using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.UI.EchoShowUI
{
    public class Template
    {
        public virtual string Type { get; set; }
        public virtual string Title { get; set; }
        public virtual string Token { get; set; }
        public virtual TextContent TextContent { get; set; }
        public virtual Image Image { get; set; }
        public virtual string BackButton { get; set; }
        public virtual Image BackgroundImage { get; set; }
    }
}
