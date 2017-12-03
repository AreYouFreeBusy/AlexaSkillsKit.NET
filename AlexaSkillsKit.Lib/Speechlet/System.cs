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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static System FromJson(JObject json)
        {
            var attributes = new Dictionary<string, string>();
            var jsonAttributes = json.Value<JObject>("attributes");
            if (jsonAttributes != null)
            {
                foreach (var attrib in jsonAttributes.Children())
                {
                    attributes.Add(attrib.Value<JProperty>().Name, attrib.Value<JProperty>().Value.ToString());
                }
            }
            return new System()
            {
                Device = Device.FromJson(json.Value<JObject>("device"))
            };
        }


        public virtual Device Device { get; set; }
    }
}
