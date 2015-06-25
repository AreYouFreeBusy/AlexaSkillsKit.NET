//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using AlexaSkillsKit.Json;
using AlexaSkillsKit.Authentication;

namespace AlexaSkillsKit.Speechlet
{
    public abstract class SpeechletAsync : ISpeechletAsync
    {
        /// <summary>
        /// Processes Alexa request AND validates request signature
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public async virtual Task<HttpResponseMessage> GetResponseAsync(HttpRequestMessage httpRequest) {
            if (!httpRequest.Headers.Contains(Sdk.SIGNATURE_CERT_URL_REQUEST_HEADER) ||
                !httpRequest.Headers.Contains(Sdk.SIGNATURE_REQUEST_HEADER)) {
                return new HttpResponseMessage(HttpStatusCode.BadRequest); // Request signature absent
            }

            string chainUrl = httpRequest.Headers.GetValues(Sdk.SIGNATURE_CERT_URL_REQUEST_HEADER).First();
            string signature = httpRequest.Headers.GetValues(Sdk.SIGNATURE_REQUEST_HEADER).First();

            var alexaBytes = await httpRequest.Content.ReadAsByteArrayAsync();
            Debug.WriteLine(httpRequest.ToLogString());
            if (!(await SpeechletRequestSignatureVerifier.VerifyRequestSignatureAsync(alexaBytes, signature, chainUrl))) {
                return new HttpResponseMessage(HttpStatusCode.BadRequest); // Failed signature verification
            }

            var alexaContent = UTF8Encoding.UTF8.GetString(alexaBytes);
            string alexaResponse = await ProcessRequestAsync(alexaContent);
            
            HttpResponseMessage httpResponse;
            if (alexaResponse == null) {
                httpResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            else if (alexaResponse == String.Empty) {
                httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            else {
                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                httpResponse.Content = new StringContent(alexaResponse, Encoding.UTF8, "application/json");
                Debug.WriteLine(httpResponse.ToLogString());
            }

            return httpResponse;
        }


        /// <summary>
        /// Processes Alexa request but does NOT validate request signature 
        /// </summary>
        /// <param name="requestContent"></param>
        /// <returns></returns>
        public async virtual Task<string> ProcessRequestAsync(string requestContent) {
            var requestEnvelope = SpeechletRequestEnvelope.FromJson(requestContent);
            return await DoProcessRequestAsync(requestEnvelope);
        }


        /// <summary>
        /// Processes Alexa request but does NOT validate request signature
        /// </summary>
        /// <param name="requestJson"></param>
        /// <returns></returns>
        public async virtual Task<string> ProcessRequestAsync(JObject requestJson) {
            var requestEnvelope = SpeechletRequestEnvelope.FromJson(requestJson);
            return await DoProcessRequestAsync(requestEnvelope);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContent"></param>
        /// <returns></returns>
        public async virtual Task<string> DoProcessRequestAsync(SpeechletRequestEnvelope requestEnvelope) {
            Session session = requestEnvelope.Session;
            SpeechletResponse response = null;

            // verify timestamp is within tolerance
            var diff = DateTime.UtcNow - requestEnvelope.Request.Timestamp;
            Debug.WriteLine("Request was timestamped {0:0.00} seconds ago.", diff.TotalSeconds);
            if (Math.Abs((decimal)diff.TotalSeconds) > Sdk.TIMESTAMP_TOLERANCE_SEC) {
                return String.Empty;
            }

            // process launch request
            if (requestEnvelope.Request is LaunchRequest) {
                var request = requestEnvelope.Request as LaunchRequest;
                if (requestEnvelope.Session.IsNew) {
                    await OnSessionStartedAsync(
                        new SessionStartedRequest(request.RequestId, request.Timestamp), session);
                }
                response = await OnLaunchAsync(request, session);
            }

            // process intent request
            else if (requestEnvelope.Request is IntentRequest) {
                var request = requestEnvelope.Request as IntentRequest;

                // Do session management prior to calling OnSessionStarted and OnIntentAsync 
                // to allow dev to change session values if behavior is not desired
                DoSessionManagement(request, session);

                if (requestEnvelope.Session.IsNew) {
                    await OnSessionStartedAsync(
                        new SessionStartedRequest(request.RequestId, request.Timestamp), session);
                }
                response = await OnIntentAsync(request, session);
            }

            // process session ended request
            else if (requestEnvelope.Request is SessionEndedRequest) {
                var request = requestEnvelope.Request as SessionEndedRequest;
                await OnSessionEndedAsync(request, session);
            }

            var responseEnvelope = new SpeechletResponseEnvelope {
                Version = requestEnvelope.Version,
                Response = response,
                SessionAttributes = session.Attributes
            };
            return responseEnvelope.ToJson();
        }


        /// <summary>
        /// 
        /// </summary>
        private void DoSessionManagement(IntentRequest request, Session session) {
            if (session.IsNew) {
                session.Attributes[Session.INTENT_SEQUENCE] = request.Intent.Name;
            }
            else {
                // if the session was started as a result of a launch request 
                // a first intent isn't yet set, so set it to the current intent
                if (!session.Attributes.ContainsKey(Session.INTENT_SEQUENCE)) {
                    session.Attributes[Session.INTENT_SEQUENCE] = request.Intent.Name;
                }
                else {
                    session.Attributes[Session.INTENT_SEQUENCE] += Session.SEPARATOR + request.Intent.Name;
                }
            }

            // Auto-session management: copy all slot values from current intent into session
            foreach (var slot in request.Intent.Slots.Values) {
                if (!String.IsNullOrEmpty(slot.Value)) session.Attributes[slot.Name] = slot.Value;
            }
        }


        public abstract Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session);
        public abstract Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
        public abstract Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
        public abstract Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
    }
}