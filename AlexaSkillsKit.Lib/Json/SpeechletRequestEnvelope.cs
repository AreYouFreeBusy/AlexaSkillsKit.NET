//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.Slu;

namespace AlexaSkillsKit.Json
{
    public class SpeechletRequestEnvelope
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static SpeechletRequestEnvelope FromJson(string content) {
            if (String.IsNullOrEmpty(content)) {
                throw new SpeechletException("Request content is empty");
            }

            JObject json = JsonConvert.DeserializeObject<JObject>(content, Sdk.DeserializationSettings);
            return FromJson(json);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static SpeechletRequestEnvelope FromJson(JObject json) {
            if (json["version"] != null && json.Value<string>("version") != Sdk.VERSION) {
                throw new SpeechletException("Request must conform to 1.0 schema.");
            }

            SpeechletRequest request;
            JObject requestJson = json.Value<JObject>("request");
            string requestType = requestJson.Value<string>("type");
            string requestId = requestJson.Value<string>("requestId");
            DateTime timestamp = requestJson.Value<DateTime>("timestamp");
            switch (requestType) {
                case "LaunchRequest":
                    request = new LaunchRequest(requestId, timestamp);
                    break;
                case "IntentRequest":
                    request = new IntentRequest(requestId, timestamp, 
                        Intent.FromJson(requestJson.Value<JObject>("intent")));
                    break;
                case "SessionStartedRequest":
                    request = new SessionStartedRequest(requestId, timestamp);
                    break;
                case "SessionEndedRequest":
                    SessionEndedRequest.ReasonEnum reason;
                    Enum.TryParse<SessionEndedRequest.ReasonEnum>(requestJson.Value<string>("reason"), out reason);
                    request = new SessionEndedRequest(requestId, timestamp, reason);
                    break;
                default:
                    throw new ArgumentException("json");
            }

            return new SpeechletRequestEnvelope {
                Request = request,
                Session = Session.FromJson(json.Value<JObject>("session")),
                Version = json.Value<string>("version")
            };
        }
        

        public virtual SpeechletRequest Request {
            get;
            set;
        }

        public virtual Session Session {
            get;
            set;
        }

        public virtual string Version {
            get;
            set;
        }
    }
}