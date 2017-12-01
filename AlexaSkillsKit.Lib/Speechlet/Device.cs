using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class Device
    {
        public SupportedInterfaces SupportedInterfaces { get; set; }

        public static Device FromJson(JObject json)
        {
            if (json != null)
            {
                return new Device
                {
                    SupportedInterfaces = SupportedInterfaces.FromJson(json.Value<JObject>("supportedInterfaces"))
                };
            }

            return null;
        }
    }
}
