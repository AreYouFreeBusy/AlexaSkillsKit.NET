//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;

namespace AlexaSkillsKit.UI
{
    public class SimpleCard : Card
    {
        public override string Type {
            get { return "Simple"; }
        }

        public virtual string Content {
            get;
            set;
        }
    }
}