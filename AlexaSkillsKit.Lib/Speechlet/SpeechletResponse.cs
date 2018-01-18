// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.UI;
using System.Collections.Generic;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-and-response-json-reference.html#response-object
    /// </summary>
    public class SpeechletResponse : ISpeechletResponse
    {
        /// <summary>
        /// Set default ShouldEndSession value to false for the sake of backwards compatibility
        /// </summary>
        public SpeechletResponse() {
            ShouldEndSession = false;
        }

        public virtual Card Card {
            get;
            set;
        }

        public virtual IEnumerable<Directive> Directives {
            get;
            set;
        }

        public virtual OutputSpeech OutputSpeech {
            get;
            set;
        }

        public virtual Reprompt Reprompt {
            get;
            set;
        }

        public virtual bool? ShouldEndSession {
            get;
            set;
        }
    }
}