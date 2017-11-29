using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.UI.EchoShowUI
{
    public class Directive
    {
        public virtual string type { get; set; }
        public virtual Template template { get; set; }
        public virtual Video videoItem { get; set; }
    }
}
