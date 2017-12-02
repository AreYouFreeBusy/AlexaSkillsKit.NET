using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet.Interfaces.Display
{
    public class DisplayInterface: ISpeechletInterface
    {
        public string TemplateVersion {
            get;
            private set;
        }

        public string MarkupVersion {
            get;
            private set;
        }

        public static DisplayInterface FromJson(JObject json)
        {
            if (json == null) return null;

            return new DisplayInterface {
                TemplateVersion = json.Value<string>("templateVersion"),
                MarkupVersion = json.Value<string>("markupVersion")
            };
        }
    }
}