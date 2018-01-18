// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

namespace AlexaSkillsKit.UI
{
    public class StandardCard : Card
    {
        public override string Type {
            get { return "Standard"; }
        }

        public virtual string Title {
            get;
            set;
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