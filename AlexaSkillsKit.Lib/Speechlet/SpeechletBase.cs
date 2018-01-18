// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Authentication;
using AlexaSkillsKit.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public class SpeechletBase : ISpeechletBase
    {
        public SpeechletService Service { get; }

        protected SpeechletBase() {
            Service = new SpeechletService(this);
        }


        /// <summary>
        /// Processes Alexa request AND validates request signature
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public HttpResponseMessage GetResponse(HttpRequestMessage httpRequest) {
            return AsyncHelpers.RunSync(async () => await Service.GetResponseAsync(httpRequest));
        }

        public async Task<HttpResponseMessage> GetResponseAsync(HttpRequestMessage httpRequest) {
            return await Service.GetResponseAsync(httpRequest);
        }


        /// <summary>
        /// Processes Alexa request but does NOT validate request signature 
        /// </summary>
        /// <param name="requestContent"></param>
        /// <returns></returns>
        public string ProcessRequest(string requestContent) {
            return AsyncHelpers.RunSync(async () => await ProcessRequestAsync(requestContent));
        }

        public async Task<string> ProcessRequestAsync(string requestContent) {
            var requestEnvelope = SpeechletRequestEnvelope.FromJson(requestContent);
            return (await Service.ProcessRequestAsync(requestEnvelope))?.ToJson();
        }


        /// <summary>
        /// Processes Alexa request but does NOT validate request signature 
        /// </summary>
        /// <param name="requestJson"></param>
        /// <returns></returns>
        public virtual string ProcessRequest(JObject requestJson) {
            return AsyncHelpers.RunSync(async () => await ProcessRequestAsync(requestJson));
        }

        public async Task<string> ProcessRequestAsync(JObject requestJson) {
            var requestEnvelope = SpeechletRequestEnvelope.FromJson(requestJson);
            return (await Service.ProcessRequestAsync(requestEnvelope))?.ToJson();
        }


        /// <summary>
        /// Opportunity to set policy for handling requests with invalid signatures and/or timestamps
        /// </summary>
        /// <returns>true if request processing should continue, otherwise false</returns>
        public virtual bool OnRequestValidation(
            SpeechletRequestValidationResult result, DateTime referenceTimeUtc, SpeechletRequestEnvelope requestEnvelope) {
            
            return result == SpeechletRequestValidationResult.OK;
        }
    }
}