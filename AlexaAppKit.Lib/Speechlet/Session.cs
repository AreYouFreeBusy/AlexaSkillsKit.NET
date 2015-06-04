//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AlexaAppKit.Speechlet
{
    public class Session
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Session FromJson(JObject json) {
            var attributes = new Dictionary<string, string>();
            if (json["attributes"] != null) {
                foreach (var attrib in json.Value<JObject>("attributes").Children()) {
                    attributes.Add(attrib.Value<JProperty>().Name, attrib.Value<JProperty>().Value.ToString());
                }
            }

            return new Session {
                SessionId = json.Value<string>("sessionId"),
                IsNew = json.Value<bool>("new"),
                User = User.FromJson(json.Value<JObject>("user")),
                Application = Application.FromJson(json.Value<JObject>("application")),
                Attributes = attributes
            };
        }

        public virtual string SessionId {
            get;
            set;
        }

        public virtual bool IsNew {
            get;
            set;
        }

        public virtual Application Application {
            get;
            set;
        }

        public virtual User User {
            get;
            set;
        }

        public virtual Dictionary<string, string> Attributes {
            get;
            set;
        }
    }
}