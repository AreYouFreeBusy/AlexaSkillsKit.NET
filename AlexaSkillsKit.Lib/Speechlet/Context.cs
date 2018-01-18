// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Interfaces.AudioPlayer;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-and-response-json-reference.html#context-object
    /// </summary>
    public class Context
    {
        public static Context FromJson(JObject json) {
            if (json == null) return null;

            return new Context {
                System = SystemState.FromJson(json.Value<JObject>("System")),
                AudioPlayer = AudioPlayerState.FromJson(json.Value<JObject>("AudioPlayer"))
            };
        }

        public SystemState System {
            get;
            private set;
        }

        public AudioPlayerState AudioPlayer {
            get;
            private set;
        }
    }
}