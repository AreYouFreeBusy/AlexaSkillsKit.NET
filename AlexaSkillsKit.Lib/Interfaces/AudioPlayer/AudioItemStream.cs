// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

namespace AlexaSkillsKit.Interfaces.AudioPlayer
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#play
    /// </summary>
    public class AudioItemStream
    {
        public virtual string Url {
            get;
            set;
        }

        public virtual string Token {
            get;
            set;
        }

        public virtual string ExpectedPreviousToken {
            get;
            set;
        }

        public virtual long OffsetInMilliseconds {
            get;
            set;
        }
    }
}