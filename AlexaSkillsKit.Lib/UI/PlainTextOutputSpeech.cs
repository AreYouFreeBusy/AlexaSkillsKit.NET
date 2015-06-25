//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;

namespace AlexaSkillsKit.UI
{
    public class PlainTextOutputSpeech : OutputSpeech
    {
        public override string Type {
            get { return "PlainText";  }
        }

        public virtual string Text {
            get;
            set;
        }
    }
}