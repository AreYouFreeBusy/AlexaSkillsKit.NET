using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public class Display
    {
        public string TemplateVersion { get; set; }
        public string MarkupVersion { get; set; }

        public static Display FromJson(JObject json)
        {
            if (json != null)
            {
                Display returnDisplay = new Display();
                returnDisplay.TemplateVersion = json.Value<string>("templateVersion");
                returnDisplay.MarkupVersion = json.Value<string>("markupVersion");
                return returnDisplay;
            }
            return null;
        }
    }
}
