using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.UI.EchoShowUI
{
    public class Image
    {
        public virtual string ContentDescription { get; set; }
        public virtual IList<Source> Sources { get; set; }
    }
}
