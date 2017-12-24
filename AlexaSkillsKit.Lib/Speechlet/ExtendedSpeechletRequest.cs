using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-and-response-json-reference.html#request-body-parameters
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#requests
    /// https://developer.amazon.com/docs/custom-skills/playback-controller-interface-reference.html#requests
    /// </summary>
    public class ExtendedSpeechletRequest : SpeechletRequest
    {
        public ExtendedSpeechletRequest(string subtype, JObject json) : base(json) {
            Subtype = subtype;
        }

        public string Subtype {
            get;
            private set;
        }
    }
}