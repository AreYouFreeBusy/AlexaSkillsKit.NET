//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Slu
{
    public class Slot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Slot FromJson(JObject json) {
            return new Slot {
                Name = json.Value<string>("name"),
                Value = json.Value<string>("value")
            };
        }
        
        public virtual string Name {
            get;
            set;
        }
        public virtual string Value {
            get;
            set;
        }
    }
}