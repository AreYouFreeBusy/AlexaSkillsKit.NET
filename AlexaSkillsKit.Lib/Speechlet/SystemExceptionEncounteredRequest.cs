using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#system-exceptionencountered
    /// </summary>
    public class SystemExceptionEncounteredRequest : ExtendedSpeechletRequest
    {
        public SystemExceptionEncounteredRequest(JObject json, string subtype) : base(json, subtype) {
            Error = Error.FromJson(json.Value<JObject>("error"));
            Cause = Cause.FromJson(json.Value<JObject>("cause"));
        }

        public Error Error {
            get;
            private set;
        }

        public Cause Cause {
            get;
            private set;
        }
    }
}