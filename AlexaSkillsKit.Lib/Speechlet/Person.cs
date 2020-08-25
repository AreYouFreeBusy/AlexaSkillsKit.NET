// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class Person
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Person FromJson(JObject json) {
            if (json == null) return null;

            return new Person{
                PersonId = json.Value<string>("personId"),
                AccessToken = json.Value<string>("accessToken")
            };
        }

        public string PersonId{
            get;
            private set;
        }

        public virtual string AccessToken{
            get;
            set;
        }

    }
}
