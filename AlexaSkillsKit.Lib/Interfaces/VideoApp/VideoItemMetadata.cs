// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

namespace AlexaSkillsKit.Interfaces.VideoApp
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/videoapp-interface-reference.html#parameters-of-response
    /// </summary>
    public class VideoItemMetadata
    {
        public virtual string Title {
            get;
            set;
        }

        public virtual string Subtitle {
            get;
            set;
        }
    }
}