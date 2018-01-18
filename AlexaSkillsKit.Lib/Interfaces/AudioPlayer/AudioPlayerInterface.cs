// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Interfaces.AudioPlayer
{
    public class AudioPlayerInterface : ISpeechletInterface
    {
        public static AudioPlayerInterface FromJson(JObject json)
        {
            if (json == null) return null;

            return new AudioPlayerInterface();
        }
    }
}