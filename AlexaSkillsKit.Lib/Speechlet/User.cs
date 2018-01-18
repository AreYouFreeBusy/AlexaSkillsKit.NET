// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static User FromJson(JObject json) {
            return new User {
                Id = json.Value<string>("userId"),
                AccessToken = json.Value<string>("accessToken")
            };
        }

        public virtual string Id {
            get;
            set;
        }

        public virtual string AccessToken {
            get;
            set;
        }
    }
}