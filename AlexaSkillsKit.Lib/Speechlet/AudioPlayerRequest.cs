using System;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#requests
    /// https://developer.amazon.com/docs/custom-skills/playback-controller-interface-reference.html#requests
    /// </summary>
    public class AudioPlayerRequest : SpeechletRequest
    {
        public AudioPlayerRequest(string requestId, DateTime timestamp, string locale, string token, long offsetInMilliseconds, string type)
            : base(requestId, timestamp, locale) {
            Token = token;
            OffsetInMilliseconds = offsetInMilliseconds;
            Type = type;
        }

        public string Token {
            get;
            private set;
        }

        public long OffsetInMilliseconds {
            get;
            private set;
        }

        public string Type {
            get;
            private set;
        }
    }
}
