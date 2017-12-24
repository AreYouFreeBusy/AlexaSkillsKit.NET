//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AlexaSkillsKit.Speechlet;
using System.Linq;

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
            var version = json.Value<string>("version");
            if (version != null && version != Sdk.VERSION) {
                throw new SpeechletException("Request must conform to 1.0 schema.");
            }

            return new SpeechletRequestEnvelope {
                Version = version,
                Request = RequestFromJson(json.Value<JObject>("request")),
                Session = Session.FromJson(json.Value<JObject>("session")),
                Context = Context.FromJson(json.Value<JObject>("context"))
            };
        }


        private static SpeechletRequest RequestFromJson(JObject json) {
            var requestTypeParts = json?.Value<string>("type")?.Split('.');
            if (requestTypeParts == null) {
                throw new ArgumentException("json");
            }

            var requestType = requestTypeParts.Length > 1 ? requestTypeParts[0] : "";
            var requestSubType = requestTypeParts.Last();

            switch (requestType) {
                case "":
                    switch (requestSubType) {
                        case "LaunchRequest":
                            return new LaunchRequest(json);
                        case "IntentRequest":
                            return new IntentRequest(json);
                        case "SessionEndedRequest":
                            return new SessionEndedRequest(json);
                    }
                    break;
                case "AudioPlayer":
                    switch (requestSubType) {
                        case "PlaybackFailed":
                            return new AudioPlayerPlaybackFailedRequest(json, requestSubType);
                        default:
                            return new AudioPlayerRequest(json, requestSubType);
                    }
                case "PlaybackController":
                    return new PlaybackControllerRequest(json, requestSubType);
                case "Display":
                    return new DisplayRequest(json, requestSubType);
                case "System":
                    switch (requestSubType) {
                        case "ExceptionEncountered":
                            return new SystemExceptionEncounteredRequest(json, requestSubType);
                    }
                    break;
            }
            throw new ArgumentException("json");
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

        public virtual Context Context {
            get;
            set;
        }
    }
}