using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.UI.EchoShowUI
{
    public class TextContent
    {
        public virtual MainContentText PrimaryText { get; set; }
        public virtual MainContentText SecondaryText { get; set; }
        public virtual MainContentText TertiaryText { get; set; }
    }
}
