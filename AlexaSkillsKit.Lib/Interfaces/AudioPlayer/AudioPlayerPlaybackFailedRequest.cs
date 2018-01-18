// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Interfaces.AudioPlayer
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#playbackfailed
    /// </summary>
    public class AudioPlayerPlaybackFailedRequest : AudioPlayerRequest
    {
        public AudioPlayerPlaybackFailedRequest(string subtype, JObject json) : base(subtype, json) {
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