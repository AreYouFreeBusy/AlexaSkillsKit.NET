using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Directives.AudioPlayer
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