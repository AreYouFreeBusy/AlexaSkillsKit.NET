using System;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-and-response-json-reference.html#request-body-parameters
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#requests
    /// https://developer.amazon.com/docs/custom-skills/playback-controller-interface-reference.html#requests
    /// </summary>
    public class ExtendedSpeechletRequest : SpeechletRequest
    {
        public ExtendedSpeechletRequest(string requestId, DateTime timestamp, string locale, string subtype)
            : base(requestId, timestamp, locale) {
            Subtype = subtype;
        }

        public string Subtype {
            get;
            private set;
        }
    }
}