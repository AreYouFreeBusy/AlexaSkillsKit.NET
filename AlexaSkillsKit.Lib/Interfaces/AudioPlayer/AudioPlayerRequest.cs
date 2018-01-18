// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Interfaces.AudioPlayer
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#requests
    /// </summary>
    public class AudioPlayerRequest : ExtendedSpeechletRequest
    {
        public static readonly string TypeName = "AudioPlayer";

        public AudioPlayerRequest(string subtype, JObject json) : base(TypeName, subtype, json) {
            Token = json.Value<string>("token");
            OffsetInMilliseconds = json.Value<long?>("offsetInMilliseconds");
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