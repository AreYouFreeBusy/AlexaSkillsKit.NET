using System;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#playbackfailed
    /// </summary>
    public class AudioPlayerPlaybackFailedRequest : AudioPlayerRequest
    {
        public AudioPlayerPlaybackFailedRequest(string requestId, DateTime timestamp, string locale, string subtype, string token, Error error, PlaybackState currentPlaybackState)
            : base(requestId, timestamp, locale, subtype, token, 0) {
            Error = error;
            CurrentPlaybackState = currentPlaybackState;
        }

        public Error Error {
            get;
            private set;
        }

        public PlaybackState CurrentPlaybackState {
            get;
            private set;
        }
    }
}