//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using AlexaAppKit.UI;

namespace AlexaAppKit.Speechlet
{
    public class SpeechletResponse
    {
        public virtual Card Card {
            get;
            set;
        }

        public virtual OutputSpeech OutputSpeech {
            get;
            set;
        }

        public virtual bool ShouldEndSession {
            get;
            set;
        }
    }
}