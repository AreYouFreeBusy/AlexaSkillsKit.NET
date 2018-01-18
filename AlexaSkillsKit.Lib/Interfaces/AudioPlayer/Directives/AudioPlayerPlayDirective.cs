// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

namespace AlexaSkillsKit.Interfaces.AudioPlayer.Directives
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#play
    /// </summary>
    public class AudioPlayerPlayDirective : AudioPlayerDirective
    {
        public AudioPlayerPlayDirective() : base("Play") {

        }

        public virtual PlayBehaviorEnum PlayBehavior {
            get;
            set;
        }

        public virtual AudioItem AudioItem {
            get;
            set;
        }

        public enum PlayBehaviorEnum
        {
            REPLACE_ALL,
            ENQUEUE,
            REPLACE_ENQUEUED
        }
    }
}