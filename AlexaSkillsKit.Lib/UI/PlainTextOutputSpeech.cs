// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

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
        
        public static implicit operator PlainTextOutputSpeech(string spokenText)
        {
            return new PlainTextOutputSpeech()
            {
                Text = spokenText
            };
        }
    }
}