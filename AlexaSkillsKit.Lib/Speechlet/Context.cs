using AlexaSkillsKit.Speechlet.Interfaces.AudioPlayer;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
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