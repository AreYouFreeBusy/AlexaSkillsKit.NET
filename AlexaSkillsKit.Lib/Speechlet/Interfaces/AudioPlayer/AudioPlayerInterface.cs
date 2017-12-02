using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet.Interfaces.AudioPlayer
{
    public class AudioPlayerInterface : ISpeechletInterface
    {
        public static AudioPlayerInterface FromJson(JObject json)
        {
            if (json == null) return null;

            return new AudioPlayerInterface();
        }
    }
}