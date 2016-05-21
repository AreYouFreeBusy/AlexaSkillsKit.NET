//  Copyright 2016 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;

namespace AlexaSkillsKit.UI
{
    public class StandardCard : Card
    {
        public override string Type {
            get { return "Standard"; }
        }

        public virtual string Text {
            get;
            set;
        }

        public virtual Image Image {
            get;
            set;
        }
    }
}