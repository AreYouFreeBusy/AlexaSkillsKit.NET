using System;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#system-exceptionencountered
    /// </summary>
    public class SystemExceptionEncounteredRequest : ExtendedSpeechletRequest
    {
        public SystemExceptionEncounteredRequest(string requestId, DateTime timestamp, string locale, string subtype, Error error, Cause cause)
            : base(requestId, timestamp, locale, subtype) {
            Error = error;
            Cause = Cause;
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