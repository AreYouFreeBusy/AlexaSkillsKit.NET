// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Interfaces.Display
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/display-interface-reference.html#determining-the-version-of-the-supported-display
    /// </summary>
    public class DisplayInterface: ISpeechletInterface
    {
        private const string DefaultTemplateVersion = "1";
        private const string DefaultMarkupVersion = "1";

        public static DisplayInterface FromJson(JObject json) {
            if (json == null) return null;

            return new DisplayInterface {
                TemplateVersion = json.Value<string>("templateVersion") ?? DefaultTemplateVersion,
                MarkupVersion = json.Value<string>("markupVersion") ?? DefaultMarkupVersion,
                Token = json.Value<string>("token")
            };
        }

        public string TemplateVersion {
            get;
            private set;
        }

        public string MarkupVersion {
            get;
            private set;
        }

        public string Token {
            get;
            private set;
        }
    }
}