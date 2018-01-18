// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;
using System.Collections.Generic;

namespace AlexaSkillsKit.UI
{
    public class SsmlOutputSpeech : OutputSpeech
    {
        public override string Type {
            get { return "SSML";  }
        }

        public virtual string Ssml {
            get;
            set;
        }
    }
}