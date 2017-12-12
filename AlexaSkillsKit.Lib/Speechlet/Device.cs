using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-and-response-json-reference.html#system-object
    /// </summary>
    public class Device
    {
        public static Device FromJson(JObject json) {
            if (json == null) return null;

            return new Device {
                DeviceId = json.Value<string>("deviceId"),
                SupportedInterfaces = SupportedInterfaces.FromJson(json.Value<JObject>("supportedInterfaces"))
            };
        }

        public string DeviceId {
            get;
            private set;
        }

        public SupportedInterfaces SupportedInterfaces {
            get;
            private set;
        }
    }
}