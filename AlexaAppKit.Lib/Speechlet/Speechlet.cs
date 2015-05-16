//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using AlexaAppKit.Json;
using AlexaAppKit.Authentication;

namespace AlexaAppKit.Speechlet
{
    public abstract class Speechlet : ISpeechlet
    {
        /// <summary>
        /// Processes Alexa request AND validates request signature
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual HttpResponseMessage GetResponse(HttpRequestMessage request) {
            if (!request.Headers.Contains(Sdk.SIGNATURE_CERT_URL_REQUEST_HEADER) ||
                !request.Headers.Contains(Sdk.SIGNATURE_REQUEST_HEADER)) {
                return new HttpResponseMessage(HttpStatusCode.BadRequest); // Request signature absent
            }

            string chainUrl = request.Headers.GetValues(Sdk.SIGNATURE_CERT_URL_REQUEST_HEADER).First();
            string signature = request.Headers.GetValues(Sdk.SIGNATURE_REQUEST_HEADER).First();

            var alexaBytes = AsyncHelpers.RunSync<byte[]>(() => request.Content.ReadAsByteArrayAsync());
            if (!SpeechletRequestSignatureVerifier.VerifyRequestSignature(alexaBytes, signature, chainUrl)) {
                return new HttpResponseMessage(HttpStatusCode.BadRequest); // Failed signature verification
            }

            var alexaContent = UTF8Encoding.UTF8.GetString(alexaBytes);
            string alexaResponse = ProcessRequest(alexaContent);

            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
            httpResponse.Content = new StringContent(alexaResponse, Encoding.UTF8, "application/json");
            return httpResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContent"></param>
        /// <returns></returns>
        public virtual string ProcessRequest(string requestContent) {
            var requestEnvelope = SpeechletRequestEnvelope.FromJson(requestContent);
            return DoProcessRequest(requestEnvelope);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestJson"></param>
        /// <returns></returns>
        public virtual string ProcessRequest(JObject requestJson) {
            var requestEnvelope = SpeechletRequestEnvelope.FromJson(requestJson);
            return DoProcessRequest(requestEnvelope);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestEnvelope"></param>
        /// <returns></returns>
        private string DoProcessRequest(SpeechletRequestEnvelope requestEnvelope) {
            SpeechletResponse response = null;
            if (requestEnvelope.Request is LaunchRequest) {
                if (requestEnvelope.Session.IsNew) {
                    OnSessionStarted(
                        new SessionStartedRequest(requestEnvelope.Request.RequestId), 
                        requestEnvelope.Session);
                }
                response = OnLaunch(requestEnvelope.Request as LaunchRequest, requestEnvelope.Session);
            }
            else if (requestEnvelope.Request is IntentRequest) {
                if (requestEnvelope.Session.IsNew) {
                    OnSessionStarted(
                        new SessionStartedRequest(requestEnvelope.Request.RequestId), 
                        requestEnvelope.Session);
                }
                response = OnIntent(requestEnvelope.Request as IntentRequest, requestEnvelope.Session);
            }
            else if (requestEnvelope.Request is SessionEndedRequest) {
                OnSessionEnded(requestEnvelope.Request as SessionEndedRequest, requestEnvelope.Session);
            }

            var responseEnvelope = new SpeechletResponseEnvelope {
                Version = requestEnvelope.Version,
                Response = response,
                SessionAttributes = requestEnvelope.Session.Attributes
            };
            return responseEnvelope.ToJson();
        }


        public abstract SpeechletResponse OnIntent(IntentRequest intentRequest, Session session);
        public abstract SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session);
        public abstract void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session);
        public abstract void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session);
    }
}