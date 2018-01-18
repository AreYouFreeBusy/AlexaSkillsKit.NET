// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Interfaces.VideoApp.Directives
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/videoapp-interface-reference.html#videoapp-directives
    /// </summary>
    public class VideoAppLaunchDirective : Directive
    {
        public VideoAppLaunchDirective() : base("VideoApp.Launch") {

        }

        public virtual VideoItem VideoItem {
            get;
            set;
        }
    }
}