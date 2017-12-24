using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#playbackfailed
    /// </summary>
    public class AudioPlayerPlaybackFailedRequest : AudioPlayerRequest
    {
        public AudioPlayerPlaybackFailedRequest(JObject json, string subtype) : base(json, subtype) {
            Error = Error.FromJson(json.Value<JObject>("error"));
            CurrentPlaybackState = PlaybackState.FromJson(json.Value<JObject>("currentPlaybackState"));
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