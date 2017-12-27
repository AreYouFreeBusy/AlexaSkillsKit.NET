using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#touch-selection-events
    /// </summary>
    public class DisplayRequest : ExtendedSpeechletRequest
    {
        public static readonly string TypeName = "Display";

        public DisplayRequest(string subtype, JObject json) : base(TypeName, subtype, json) {
            Token = json.Value<string>("token");
        }

        public string Token {
            get;
            private set;
        }
    }
}