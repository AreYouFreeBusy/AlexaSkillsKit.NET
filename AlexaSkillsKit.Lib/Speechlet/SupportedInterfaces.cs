using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public class SupportedInterfaces
    {
        public Display Display { get; set; }
 
        public static SupportedInterfaces FromJson(JObject json)
        {
            if (json != null)
            {
                SupportedInterfaces returnSupportedInterfaces = new SupportedInterfaces();
                returnSupportedInterfaces.Display = Display.FromJson(json.Value<JObject>("audioPlayer"));
                return returnSupportedInterfaces;
            }
            return null;
        }
    }
}
