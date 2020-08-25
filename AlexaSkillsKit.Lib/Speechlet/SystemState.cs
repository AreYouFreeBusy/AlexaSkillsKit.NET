// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-and-response-json-reference.html#system-object
    /// </summary>
    public class SystemState : ISpeechletInterfaceState
    {
        public static SystemState FromJson(JObject json) {
            if (json == null) return null;

            return new SystemState {
                Application = Application.FromJson(json.Value<JObject>("application")),
                User = User.FromJson(json.Value<JObject>("user")),
                Device = Device.FromJson(json.Value<JObject>("device")),
                Person = Person.FromJson(json.Value<JObject>("person")),
                ApiEndpoint = json.Value<string>("apiEndpoint"),
                ApiAccessToken = json.Value<string>("apiAccessToken")
            };
        }

        public Application Application {
            get;
            private set;
        }

        public User User {
            get;
            private set;
        }

        public Person Person {
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
    }
}
