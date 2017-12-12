namespace AlexaSkillsKit.Interfaces.AudioPlayer
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#play
    /// </summary>
    public class AudioItem
    {
        public virtual AudioItemStream Stream {
            get;
            set;
        }
    }
}