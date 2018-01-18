// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Interfaces.AudioPlayer.Directives;
using AlexaSkillsKit.Speechlet;
using System.Collections.Generic;

namespace AlexaSkillsKit.Interfaces.AudioPlayer
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-and-response-json-reference.html#response-to-audioplayer-or-playbackcontroller-example-directives-only
    /// </summary>
    public class AudioPlayerResponse : ISpeechletResponse
    {
        public virtual IEnumerable<AudioPlayerDirective> Directives {
            get;
            set;
        }
    }
}