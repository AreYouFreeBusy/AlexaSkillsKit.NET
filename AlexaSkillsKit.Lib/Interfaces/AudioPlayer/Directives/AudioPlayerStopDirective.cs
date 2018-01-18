// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

namespace AlexaSkillsKit.Interfaces.AudioPlayer.Directives
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#stop
    /// </summary>
    public class AudioPlayerStopDirective : AudioPlayerDirective
    {
        public AudioPlayerStopDirective() : base("Stop") {

        }
    }
}