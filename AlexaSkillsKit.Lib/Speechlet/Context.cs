using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class Context
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Context FromJson(JObject json)
        {
            var attributes = new Dictionary<string, string>();
            var jsonAttributes = json.Value<JObject>("attributes");
            if (jsonAttributes != null)
            {
                foreach (var attrib in jsonAttributes.Children()) { 
                    attributes.Add(attrib.Value<JProperty>().Name, attrib.Value<JProperty>().Value.ToString());
                }
            }

            return new Context
            {
                System = System.FromJson(json.Value<JObject>("System"))
            };
        }

        public virtual System System
        {
            get;
            set;
        }
         
    }

}
