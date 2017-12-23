//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public static class SystemNetHttpExtensions
    {
        /// <summary>
        /// Processes Alexa request AND validates request signature
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetResponseAsync(this SpeechletService service, HttpRequestMessage httpRequest) {
            string chainUrl = null;
            if (!httpRequest.Headers.Contains(Sdk.SIGNATURE_CERT_URL_REQUEST_HEADER)) {
                chainUrl = httpRequest.Headers.GetValues(Sdk.SIGNATURE_CERT_URL_REQUEST_HEADER).FirstOrDefault(x => !string.IsNullOrEmpty(x));
            }

            string signature = null;
            if (!httpRequest.Headers.Contains(Sdk.SIGNATURE_REQUEST_HEADER)) {
                signature = httpRequest.Headers.GetValues(Sdk.SIGNATURE_REQUEST_HEADER).FirstOrDefault(x => !string.IsNullOrEmpty(x));
            }

            var content = await httpRequest.Content.ReadAsStringAsync();

            try {
                var alexaRequest = await service.GetRequestAsync(content, chainUrl, signature);
                var alexaResponse = await service.ProcessRequestAsync(alexaRequest);
                var json = alexaResponse?.ToJson();

                return (json == null) ?
                    new HttpResponseMessage(HttpStatusCode.InternalServerError) :
                    new HttpResponseMessage(HttpStatusCode.OK) {
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    };
            }
            catch (SpeechletValidationException ex) {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) {
                    ReasonPhrase = ex.ValidationResult.ToString()
                };
            }
        }

        /// <summary>
        /// Processes Alexa request AND validates request signature
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static HttpResponseMessage GetResponse(this SpeechletBase speechlet, HttpRequestMessage httpRequest) {
            return AsyncHelpers.RunSync(async () => await speechlet.Service.GetResponseAsync(httpRequest));
        }

        public static async Task<HttpResponseMessage> GetResponseAsync(this SpeechletBase speechlet, HttpRequestMessage httpRequest) {
            return await speechlet.Service.GetResponseAsync(httpRequest);
        }
    }
}