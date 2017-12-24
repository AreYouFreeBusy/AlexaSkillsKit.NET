using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#requests
    /// </summary>
    public class AudioPlayerRequest : ExtendedSpeechletRequest
    {
        public AudioPlayerRequest(JObject json, string subtype) : base(json, subtype) {
            Token = json.Value<string>("token");
            OffsetInMilliseconds = json.Value<long?>("offsetInMilliseconds");
        }

        public string Token {
            get;
            private set;
        }

        public long? OffsetInMilliseconds {
            get;
            private set;
        }
    }
}