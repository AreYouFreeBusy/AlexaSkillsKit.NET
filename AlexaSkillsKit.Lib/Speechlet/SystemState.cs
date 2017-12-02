using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class SystemState : ISpeechletInterfaceState
    {
        public Application Application {
            get;
            private set;
        }

        public User User {
            get;
            private set;
        }

        public Device Device {
            get;
            private set;
        }

        public string ApiEndpoint {
            get;
            private set;
        }

        public string ApiAccessToken {
            get;
            private set;
        }

        public static SystemState FromJson(JObject json) {
            if (json == null) return null;

            return new SystemState {
                Application = Application.FromJson(json.Value<JObject>("application")),
                User = User.FromJson(json.Value<JObject>("user")),
                Device = Device.FromJson(json.Value<JObject>("device")),
                ApiEndpoint = json.Value<string>("apiEndpoint"),
                ApiAccessToken = json.Value<string>("apiAccessToken")
            };
        }
    }
}