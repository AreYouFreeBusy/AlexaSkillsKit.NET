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
using AlexaAppKit.Json;
using AlexaAppKit.Authentication;

namespace AlexaAppKit.Speechlet
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

            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
            httpResponse.Content = new StringContent(alexaResponse, Encoding.UTF8, "application/json");
            Debug.WriteLine(httpResponse.ToLogString());
            
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
            SpeechletResponse response = null;
            if (requestEnvelope.Request is LaunchRequest) {
                if (requestEnvelope.Session.IsNew) {
                    await OnSessionStartedAsync(
                        new SessionStartedRequest(requestEnvelope.Request.RequestId),
                        requestEnvelope.Session);
                }
                response = await OnLaunchAsync(requestEnvelope.Request as LaunchRequest, requestEnvelope.Session);
            }
            else if (requestEnvelope.Request is IntentRequest) {
                if (requestEnvelope.Session.IsNew) {
                    await OnSessionStartedAsync(
                        new SessionStartedRequest(requestEnvelope.Request.RequestId),
                        requestEnvelope.Session);
                }
                response = await OnIntentAsync(requestEnvelope.Request as IntentRequest, requestEnvelope.Session);
            }
            else if (requestEnvelope.Request is SessionEndedRequest) {
                await OnSessionEndedAsync(requestEnvelope.Request as SessionEndedRequest, requestEnvelope.Session);
            }

            var responseEnvelope = new SpeechletResponseEnvelope {
                Version = requestEnvelope.Version,
                Response = response,
                SessionAttributes = requestEnvelope.Session.Attributes
            };
            return responseEnvelope.ToJson();
        }


        public abstract Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session);
        public abstract Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
        public abstract Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
        public abstract Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
    }
}