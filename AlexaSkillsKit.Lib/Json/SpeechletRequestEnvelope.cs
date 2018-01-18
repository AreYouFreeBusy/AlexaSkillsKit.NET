// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.Authentication;

namespace AlexaSkillsKit.Json
{
    public class SpeechletRequestEnvelope
    {
        public static SpeechletRequestParser RequestParser { get; } = new SpeechletRequestParser();

        static SpeechletRequestEnvelope() {
            RequestParser.AddStandard();
            RequestParser.AddSystem();
            RequestParser.AddAudioPlayer();
            RequestParser.AddPlaybackController();
            RequestParser.AddDisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static SpeechletRequestEnvelope FromJson(string content) {
            if (String.IsNullOrEmpty(content)) {
                throw new SpeechletValidationException(SpeechletRequestValidationResult.NoContent, "Request content is empty");
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
                throw new SpeechletValidationException(SpeechletRequestValidationResult.InvalidVersion, "Request must conform to 1.0 schema.");
            }

            return new SpeechletRequestEnvelope {
                Version = version,
                Request = RequestParser.Parse(json.Value<JObject>("request")),
                Session = Session.FromJson(json.Value<JObject>("session")),
                Context = Context.FromJson(json.Value<JObject>("context"))
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

        public virtual Context Context {
            get;
            set;
        }
    }
}