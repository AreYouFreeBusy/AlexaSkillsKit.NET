using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillsKit.Slu;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class SupportedInterfaces
    {
        public AudioPlayer AudioPlayer { get; set; }

        public static SupportedInterfaces FromJson(JObject json)
        {
            if (json != null)
            {
                return new SupportedInterfaces
                {
                    AudioPlayer = AudioPlayer.FromJson(json.Value<JObject>("audioPlayer"))
                };
            }

            return null;
        }
    }
}