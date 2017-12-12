using AlexaSkillsKit.Speechlet;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Interfaces.AudioPlayer
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