using AlexaSkillsKit.Speechlet.Interfaces.AudioPlayer;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class Context
    {
        public SystemState System { get; set; }
        public AudioPlayerState AudioPlayer { get; set; }

        public static Context FromJson(JObject json)
        {
            if (json != null)
            {
                return new Context
                {
                    System = SystemState.FromJson(json.Value<JObject>("System")),
                    AudioPlayer = AudioPlayerState.FromJson(json.Value<JObject>("AudioPlayer"))
                };
            }

            return null;
        }
    }
}
