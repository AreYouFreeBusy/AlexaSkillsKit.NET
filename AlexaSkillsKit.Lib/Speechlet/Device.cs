using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public class Device
    {
        public SupportedInterfaces SupportedInterfaces { get; set; }

        public static Device FromJson(JObject json)
        {
            if (json != null)
            {
                Device returnDevice = new Device();
                returnDevice.SupportedInterfaces = SupportedInterfaces.FromJson(json.Value<JObject>("supportedInterfaces"));
                return returnDevice;
            }
            return null;
        }
    }
}
