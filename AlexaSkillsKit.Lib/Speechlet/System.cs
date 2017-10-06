using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                System returnSystem = new System();
                returnSystem.Application = Application.FromJson(json.Value<JObject>("application"));
                returnSystem.User = User.FromJson(json.Value<JObject>("user"));
                returnSystem.Device = Device.FromJson(json.Value<JObject>("device"));
                return returnSystem;
            }
            return null;
        }
    }
}
