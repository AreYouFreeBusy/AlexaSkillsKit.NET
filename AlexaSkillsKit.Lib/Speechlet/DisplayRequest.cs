using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#touch-selection-events
    /// </summary>
    public class DisplayRequest : ExtendedSpeechletRequest
    {
        public DisplayRequest(string subtype, JObject json) : base(subtype, json) {
            Token = json.Value<string>("token");
        }

        public string Token {
            get;
            private set;
        }
    }
}