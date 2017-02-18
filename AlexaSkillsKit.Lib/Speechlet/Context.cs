using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillsKit.Slu;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class Context
    {
        public System System { get; set; }
        public AudioPlayer AudioPlayer { get; set; }

        public static Context FromJson(JObject json)
        {
            if (json != null)
            {
                return new Context
                {
                    System = System.FromJson(json.Value<JObject>("System")),
                    AudioPlayer = AudioPlayer.FromJson(json.Value<JObject>("AudioPlayer"))
                };
            }

            return null;
        }
    }
}
