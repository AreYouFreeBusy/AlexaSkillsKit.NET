using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class System
    {
        public Application Application { get; private set; }
        public User User { get; set; }
        public Device Device { get; set; }

        public static System FromJson(JObject json)
        {
            if (json != null)
            {
                return new System
                {
                    Application = Application.FromJson(json.Value<JObject>("application")),
                    User = User.FromJson(json.Value<JObject>("user")),
                    Device = Device.FromJson(json.Value<JObject>("device"))
                };
            }

            return null;
        }
    }
}
