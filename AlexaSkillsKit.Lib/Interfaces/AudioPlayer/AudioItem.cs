// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

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