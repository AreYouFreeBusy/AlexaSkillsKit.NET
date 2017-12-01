using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public class Context
    {
        public System System { get; set; }
  
        public static Context FromJson(JObject json)
        {
            if (json != null)
            {
                Context returnContext = new Context();
                returnContext.System = System.FromJson(json.Value<JObject>("System"));
                return returnContext;               
            }
            return null;
        }
    }
}
