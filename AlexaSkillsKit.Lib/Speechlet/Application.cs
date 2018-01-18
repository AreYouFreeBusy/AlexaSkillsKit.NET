// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class Application
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Application FromJson(JObject json) {
            return new Application {
                Id = json.Value<string>("applicationId")
            };
        }

        public virtual string Id {
            get;
            set;
        }
    }
}