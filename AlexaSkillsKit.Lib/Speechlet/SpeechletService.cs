//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using AlexaSkillsKit.Authentication;
using AlexaSkillsKit.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public class SpeechletService
    {
        private ISpeechletAsync speechlet;

        public void Initialize(ISpeechletAsync speechlet) {
            this.speechlet = speechlet;
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
            catch (Exception ex)
            when (ex is Newtonsoft.Json.JsonReaderException || ex is InvalidCastException || ex is FormatException) {
                validationResult |= SpeechletRequestValidationResult.InvalidJson;
            }

            var success = false;

            // attempt to verify timestamp only if we were able to parse request body
            if (result != null) {
                var now = DateTime.UtcNow; // reference time for this request

                if (!SpeechletRequestTimestampVerifier.VerifyRequestTimestamp(result, now)) {
                    validationResult |= SpeechletRequestValidationResult.InvalidTimestamp;
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
            ISpeechletResponse response = null;

            if (session != null) {
                // Do session management prior to calling OnSessionStarted and OnIntentAsync 
                // to allow dev to change session values if behavior is not desired
                DoSessionManagement(request as IntentRequest, session);

                if (session.IsNew) {
                    await speechlet?.OnSessionStartedAsync(
                        new SessionStartedRequest(request.RequestId, request.Timestamp, request.Locale), session);
                }
            }

            // process launch request
            if (requestEnvelope.Request is LaunchRequest) {
                response = await speechlet?.OnLaunchAsync(request as LaunchRequest, session, context);
            }

            // process audio player request
            else if (requestEnvelope.Request is AudioPlayerRequest) {
                response = await speechlet?.OnAudioPlayerAsync(request as AudioPlayerRequest, context);
            }

            // process playback controller request
            else if (requestEnvelope.Request is PlaybackControllerRequest) {
                response = await speechlet?.OnPlaybackControllerAsync(request as PlaybackControllerRequest, context);
            }

            // process display request
            else if (requestEnvelope.Request is DisplayRequest) {
                response = await speechlet?.OnDisplayAsync(request as DisplayRequest, context);
            }

            // process system request
            else if (requestEnvelope.Request is SystemExceptionEncounteredRequest) {
                await speechlet?.OnSystemExceptionEncounteredAsync(request as SystemExceptionEncounteredRequest, context);
            }

            // process intent request
            else if (requestEnvelope.Request is IntentRequest) {
                response = await speechlet?.OnIntentAsync(request as IntentRequest, session, context);
            }

            // process session ended request
            else if (requestEnvelope.Request is SessionEndedRequest) {
                await speechlet?.OnSessionEndedAsync(request as SessionEndedRequest, session);
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