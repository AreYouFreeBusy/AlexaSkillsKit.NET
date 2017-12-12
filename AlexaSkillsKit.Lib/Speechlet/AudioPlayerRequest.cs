using System;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#requests
    /// </summary>
    public class AudioPlayerRequest : ExtendedSpeechletRequest
    {
        public AudioPlayerRequest(string requestId, DateTime timestamp, string locale, string subtype, string token, long offsetInMilliseconds)
            : base(requestId, timestamp, locale, subtype) {
            Token = token;
            OffsetInMilliseconds = offsetInMilliseconds;
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