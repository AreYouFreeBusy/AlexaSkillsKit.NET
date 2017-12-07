using System;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#touch-selection-events
    /// </summary>
    public class DisplayRequest : ExtendedSpeechletRequest
    {
        public DisplayRequest(string requestId, DateTime timestamp, string locale, string subtype, string token)
            : base(requestId, timestamp, locale, subtype) {
            Token = token;
        }

        public string Token {
            get;
            private set;
        }
    }
}