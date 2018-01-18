// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

namespace AlexaSkillsKit.Interfaces.AudioPlayer.Directives
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#clearqueue
    /// </summary>
    public class AudioPlayerClearQueueDirective : AudioPlayerDirective
    {
        public AudioPlayerClearQueueDirective() : base("ClearQueue") {

        }

        public virtual ClearBehaviorEnum ClearBehavior {
            get;
            set;
        }

        public enum ClearBehaviorEnum
        {
            CLEAR_ENQUEUED,
            CLEAR_ALL
        }
    }
}