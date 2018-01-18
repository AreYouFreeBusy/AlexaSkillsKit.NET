// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

namespace AlexaSkillsKit.UI
{
    public class SimpleCard : Card
    {
        public override string Type {
            get { return "Simple"; }
        }

        public virtual string Title {
            get;
            set;
        }

        public virtual string Content {
            get;
            set;
        }
    }
}