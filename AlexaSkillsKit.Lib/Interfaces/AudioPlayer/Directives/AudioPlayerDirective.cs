// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Interfaces.AudioPlayer.Directives
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#directives
    /// </summary>
    public class AudioPlayerDirective : Directive
    {
        public AudioPlayerDirective(string subtype) : base($"AudioPlayer.{subtype}") {

        }
    }
}