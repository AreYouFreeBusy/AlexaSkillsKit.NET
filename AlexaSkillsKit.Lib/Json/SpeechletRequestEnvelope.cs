//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AlexaSkillsKit.Helpers;
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
            SpeechletRequest request;
            var requestTypeParts = json?.Value<string>("type")?.Split('.');
            if (requestTypeParts == null) {
                throw new ArgumentException("json");
            }

            var requestType = requestTypeParts[0];
            var requestSubType = requestTypeParts.Length > 1 ? requestTypeParts[1] : null;

            var requestId = json.Value<string>("requestId");
            var timestamp = DateTimeHelpers.FromAlexaTimestamp(json);
            var locale = json.Value<string>("locale");
            switch (requestType) {
                case "LaunchRequest":
                    request = new LaunchRequest(requestId, timestamp, locale);
                    break;
                case "IntentRequest":
                    IntentRequest.DialogStateEnum dialogState = IntentRequest.DialogStateEnum.NONE;
                    Enum.TryParse(json.Value<string>("dialogState"), out dialogState);
                    var intent = Intent.FromJson(json.Value<JObject>("intent"));
                    request = new IntentRequest(requestId, timestamp, locale, intent, dialogState);
                    break;
                case "SessionStartedRequest":
                    request = new SessionStartedRequest(requestId, timestamp, locale);
                    break;
                case "SessionEndedRequest":
                    SessionEndedRequest.ReasonEnum reason = SessionEndedRequest.ReasonEnum.NONE;
                    Enum.TryParse(json.Value<string>("reason"), out reason);
                    var sessionError = Error.FromJson(json.Value<JObject>("error"));
                    request = new SessionEndedRequest(requestId, timestamp, locale, reason, sessionError);
                    break;
                case "AudioPlayer":
                    var token = json.Value<string>("token");
                    var offset = json.Value<long?>("offsetInMilliseconds");
                    var playbackError = Error.FromJson(json.Value<JObject>("error"));
                    var currentPlaybackState = PlaybackState.FromJson(json.Value<JObject>("currentPlaybackState"));
                    switch (requestSubType) {
                        case "PlaybackFailed":
                            request = new AudioPlayerPlaybackFailedRequest(requestId, timestamp, locale, requestSubType, token, playbackError, currentPlaybackState);
                            break;
                        default:
                            request = new AudioPlayerRequest(requestId, timestamp, locale, requestSubType, token, offset);
                            break;
                    }
                    break;
                case "PlaybackController":
                    request = new PlaybackControllerRequest(requestId, timestamp, locale, requestSubType);
                    break;
                case "Display":
                    var listItemToken = json.Value<string>("token");
                    request = new DisplayRequest(requestId, timestamp, locale, requestSubType, listItemToken);
                    break;
                case "System":
                    switch (requestSubType) {
                        case "ExceptionEncountered":
                            var systemError = Error.FromJson(json.Value<JObject>("error"));
                            var cause = Cause.FromJson(json.Value<JObject>("cause"));
                            request = new SystemExceptionEncounteredRequest(requestId, timestamp, locale, requestSubType, systemError, cause);
                            break;
                        default:
                            throw new ArgumentException("json");
                    }
                    break;
                default:
                    throw new ArgumentException("json");
            }

            return request;
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