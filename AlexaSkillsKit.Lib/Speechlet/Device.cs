using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class Device
    {
        public static Device FromJson(JObject json) {
            if (json == null) return null;

            return new Device {
                SupportedInterfaces = SupportedInterfaces.FromJson(json.Value<JObject>("supportedInterfaces"))
            };
        }

        public SupportedInterfaces SupportedInterfaces {
            get;
            private set;
        }
    }
}