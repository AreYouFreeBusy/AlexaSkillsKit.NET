namespace AlexaSkillsKit.Directives.AudioPlayer
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#clearqueue
    /// </summary>
    public class AudioPlayerClearQueueDirective : Directive
    {
        public AudioPlayerClearQueueDirective() : base("AudioPlayer.ClearQueue") {

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