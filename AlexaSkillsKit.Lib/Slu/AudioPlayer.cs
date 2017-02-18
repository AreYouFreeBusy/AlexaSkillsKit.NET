using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Slu
{
    public class AudioPlayer
    {
        public string Token { get; set; }

        public string PlayerActivity { get; set; }
        public long OffsetInMilliseconds { get; set; }

        public static AudioPlayer FromJson(JObject json)
        {
            if (json != null)
            {
                return new AudioPlayer
                {
                    Token = json.Value<string>("token"),
                    OffsetInMilliseconds = json.Value<long>("offsetInMilliseconds"),
                    PlayerActivity = json.Value<string>("playerActivity")
                };
            }

            return null;
        }
    }
}
