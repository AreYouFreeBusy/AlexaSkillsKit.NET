// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Authentication;
using AlexaSkillsKit.Interfaces.AudioPlayer;
using AlexaSkillsKit.Interfaces.Display;
using AlexaSkillsKit.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public class SpeechletService
    {
        private ISpeechletBase speechlet;

        private IDictionary<string, Func<ExtendedSpeechletRequest, Context, Task<ISpeechletResponse>>> handlers
            = new Dictionary<string, Func<ExtendedSpeechletRequest, Context, Task<ISpeechletResponse>>>();

        public string ApplicationId { get; set; }


        public void AddHandler<T>(string type, Func<T, Context, Task<ISpeechletResponse>> handler) where T : SpeechletRequest {
            handlers[type] = (request, context) => handler(request as T, context);
        }


        public SpeechletService(ISpeechletBase speechlet) {
            this.speechlet = speechlet;

            if (speechlet is IAudioPlayerSpeechletAsync || speechlet is IAudioPlayerSpeechlet) {
                AddHandler<AudioPlayerRequest>(AudioPlayerRequest.TypeName, async (request, context) => {
                    return (speechlet as IAudioPlayerSpeechlet)?.OnAudioPlayer(request, context) ??
                        await (speechlet as IAudioPlayerSpeechletAsync).OnAudioPlayerAsync(request, context);
                });

                AddHandler<PlaybackControllerRequest>(PlaybackControllerRequest.TypeName, async (request, context) => {
                    return (speechlet as IAudioPlayerSpeechlet)?.OnPlaybackController(request, context) ??
                        await (speechlet as IAudioPlayerSpeechletAsync).OnPlaybackControllerAsync(request, context);
                });

                AddHandler<SystemExceptionEncounteredRequest>(SystemRequest.TypeName, async (request, context) => {
                    (speechlet as IAudioPlayerSpeechlet)?.OnSystemExceptionEncountered(request, context);
                    await (speechlet as IAudioPlayerSpeechletAsync).OnSystemExceptionEncounteredAsync(request, context);
                    return null;
                });
            }

            if (speechlet is IDisplaySpeechletAsync) {
                AddHandler<DisplayRequest>(DisplayRequest.TypeName, async (request, context) => {
                    return (speechlet as IDisplaySpeechlet)?.OnDisplay(request, context) ??
                        await (speechlet as IDisplaySpeechletAsync).OnDisplayAsync(request, context);
                });
            }
        }


        /// <summary>
        /// Processes Alexa request AND validates request signature
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetResponseAsync(HttpRequestMessage httpRequest) {
            string chainUrl = null;
            if (httpRequest.Headers.Contains(Sdk.SIGNATURE_CERT_URL_REQUEST_HEADER)) {
                chainUrl = httpRequest.Headers.GetValues(Sdk.SIGNATURE_CERT_URL_REQUEST_HEADER).FirstOrDefault(x => !string.IsNullOrEmpty(x));
            }

            string signature = null;
            if (httpRequest.Headers.Contains(Sdk.SIGNATURE_REQUEST_HEADER)) {
                signature = httpRequest.Headers.GetValues(Sdk.SIGNATURE_REQUEST_HEADER).FirstOrDefault(x => !string.IsNullOrEmpty(x));
            }

            var content = await httpRequest.Content.ReadAsStringAsync();

            try {
                var alexaRequest = await GetRequestAsync(content, chainUrl, signature);
                var alexaResponse = await ProcessRequestAsync(alexaRequest);
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


        public async Task<SpeechletRequestEnvelope> GetRequestAsync(string content, string chainUrl, string signature) {
            var validationResult = SpeechletRequestValidationResult.OK;

            if (string.IsNullOrEmpty(chainUrl)) {
                validationResult |= SpeechletRequestValidationResult.NoCertHeader;
            }

            if (string.IsNullOrEmpty(signature)) {
                validationResult |= SpeechletRequestValidationResult.NoSignatureHeader;
            }

            // attempt to verify signature only if we were able to locate certificate and signature headers
            if (validationResult == SpeechletRequestValidationResult.OK) {
                var alexaBytes = Encoding.UTF8.GetBytes(content);

                if (!await SpeechletRequestSignatureVerifier.VerifyRequestSignatureAsync(alexaBytes, signature, chainUrl)) {
                    validationResult |= SpeechletRequestValidationResult.InvalidSignature;
                }
            }

            SpeechletRequestEnvelope result = null;
            try {
                result = SpeechletRequestEnvelope.FromJson(content);
            }
            catch (SpeechletValidationException ex) {
                validationResult |= ex.ValidationResult;
            }
            catch (Exception ex)
            when (ex is JsonReaderException || ex is InvalidCastException || ex is FormatException) {
                validationResult |= SpeechletRequestValidationResult.InvalidJson;
            }

            var success = false;

            // attempt to verify timestamp only if we were able to parse request body
            if (result != null) {
                var now = DateTime.UtcNow; // reference time for this request

                if (!SpeechletRequestTimestampVerifier.VerifyRequestTimestamp(result, now)) {
                    validationResult |= SpeechletRequestValidationResult.InvalidTimestamp;
                }

                if (!string.IsNullOrEmpty(ApplicationId) && result.Context.System.Application.Id != ApplicationId) {
                    validationResult |= SpeechletRequestValidationResult.InvalidApplicationId;
                }

                success = speechlet?.OnRequestValidation(validationResult, now, result) ?? (validationResult == SpeechletRequestValidationResult.OK);
            }

            if (!success) {
                throw new SpeechletValidationException(validationResult);
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestEnvelope"></param>
        /// <returns></returns>
        public async Task<SpeechletResponseEnvelope> ProcessRequestAsync(SpeechletRequestEnvelope requestEnvelope) {
            var session = requestEnvelope.Session;
            var context = requestEnvelope.Context;
            var request = requestEnvelope.Request;

            var response = !(request is ExtendedSpeechletRequest) ?
                await HandleStandardRequestAsync(request, session, context) :
                await HandleExtendedRequestAsync(request as ExtendedSpeechletRequest, context);

            if (response == null) {
                response = new SpeechletResponse();
            }

            var responseEnvelope = new SpeechletResponseEnvelope {
                Version = requestEnvelope.Version,
                Response = response,
                SessionAttributes = session?.Attributes
            };

            return responseEnvelope;
        }


        /// <summary>
        /// 
        /// </summary>
        private async Task<ISpeechletResponse> HandleStandardRequestAsync(
            SpeechletRequest request, Session session, Context context) {
            
            #pragma warning disable 612, 618
            if (session != null) {
                // Do session management prior to calling OnSessionStarted and OnIntentAsync 
                // to allow dev to change session values if behavior is not desired
                DoSessionManagement(request as IntentRequest, session);

                if (session.IsNew) {
                    var sessionStartedRequest = new SessionStartedRequest(request);
                    if (speechlet is ISpeechletWithContext)
                        (speechlet as ISpeechletWithContext).OnSessionStarted(sessionStartedRequest, session, context);
                    else if (speechlet is ISpeechletWithContextAsync)
                        await (speechlet as ISpeechletWithContextAsync).OnSessionStartedAsync(sessionStartedRequest, session, context);
                    else if (speechlet is ISpeechlet)
                        (speechlet as ISpeechlet).OnSessionStarted(sessionStartedRequest, session);
                    else if (speechlet is ISpeechletAsync)
                        await (speechlet as ISpeechletAsync).OnSessionStartedAsync(sessionStartedRequest, session);
                }
            }
            #pragma warning restore 612, 618

            #pragma warning disable 612, 618
            if (request is LaunchRequest) {
                // process launch request
                if (speechlet is ISpeechletWithContext)
                    return (speechlet as ISpeechletWithContext).OnLaunch(request as LaunchRequest, session, context);
                else if (speechlet is ISpeechletWithContextAsync)
                    return await (speechlet as ISpeechletWithContextAsync).OnLaunchAsync(request as LaunchRequest, session, context);
                else if (speechlet is ISpeechlet)
                    return (speechlet as ISpeechlet).OnLaunch(request as LaunchRequest, session);
                else if (speechlet is ISpeechletAsync)
                    return await (speechlet as ISpeechletAsync).OnLaunchAsync(request as LaunchRequest, session);
            }
            else if (request is IntentRequest) {
                // process intent request
                if (speechlet is ISpeechletWithContext)
                    return (speechlet as ISpeechletWithContext).OnIntent(request as IntentRequest, session, context);
                else if (speechlet is ISpeechletWithContextAsync)
                    return await (speechlet as ISpeechletWithContextAsync).OnIntentAsync(request as IntentRequest, session, context);
                else if (speechlet is ISpeechlet)
                    return (speechlet as ISpeechlet).OnIntent(request as IntentRequest, session);
                else if (speechlet is ISpeechletAsync)
                    return await (speechlet as ISpeechletAsync).OnIntentAsync(request as IntentRequest, session);
            }
            else if (request is SessionEndedRequest) {
                // process session ended request
                if (speechlet is ISpeechletWithContext)
                    (speechlet as ISpeechletWithContext).OnSessionEnded(request as SessionEndedRequest, session, context);
                else if (speechlet is ISpeechletWithContextAsync)
                    await (speechlet as ISpeechletWithContextAsync).OnSessionEndedAsync(request as SessionEndedRequest, session, context);
                else if (speechlet is ISpeechlet)
                    (speechlet as ISpeechlet).OnSessionEnded(request as SessionEndedRequest, session);
                else if (speechlet is ISpeechletAsync)
                    await (speechlet as ISpeechletAsync).OnSessionEndedAsync(request as SessionEndedRequest, session);
            }
            #pragma warning restore 612, 618

            return null;
        }


        private async Task<ISpeechletResponse> HandleExtendedRequestAsync(ExtendedSpeechletRequest request, Context context) {
            return handlers.ContainsKey(request.Type) ? (await handlers[request.Type].Invoke(request, context)) : null;
        }


        /// <summary>
        /// 
        /// </summary>
        private void DoSessionManagement(IntentRequest request, Session session) {
            if (request == null) return;

            if (session.Attributes == null) {
                session.Attributes = new Dictionary<string, string>();
            }

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
                session.Attributes[slot.Name] = slot.Value;
            }
        }
    }
}