// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;
using Newtonsoft.Json.Linq;
using System;

namespace AlexaSkillsKit.Interfaces.AudioPlayer
{
    public class AudioPlayerState : ISpeechletInterfaceState
    {
        public static AudioPlayerState FromJson(JObject json) {
            if (json == null) return null;

            PlayerActivityEnum playerActivity = PlayerActivityEnum.NONE;
            Enum.TryParse(json.Value<string>("playerActivity"), out playerActivity);
            return new AudioPlayerState {
                OffsetInMilliseconds = json.Value<long?>("offsetInMilliseconds"),
                Token = json.Value<string>("token"),
                PlayerActivity = playerActivity
            };
        }

        public long? OffsetInMilliseconds {
            get;
            private set;
        }

        public string Token {
            get;
            private set;
        }

        public PlayerActivityEnum PlayerActivity {
            get;
            private set;
        }

        public enum PlayerActivityEnum
        {
            NONE = 0, // default in case parsing fails
            PLAYING,
            PAUSED,
            FINISHED,
            BUFFER_UNDERRUN,
            IDLE,
            STOPPED
        }
    }
}