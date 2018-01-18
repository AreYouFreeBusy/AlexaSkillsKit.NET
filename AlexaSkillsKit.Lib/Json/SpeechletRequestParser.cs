// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlexaSkillsKit.Json
{
    public class SpeechletRequestParser
    {
        private IDictionary<string, Func<string, JObject, SpeechletRequest>> resolvers
            = new Dictionary<string, Func<string, JObject, SpeechletRequest>>();

        private SpeechletRequest Parse(string type, string subtype, JObject json) {
            if (json == null || !resolvers.ContainsKey(type)) return null;

            return resolvers[type](subtype, json);
        }

        public SpeechletRequest Parse(JObject json) {
            var requestTypeParts = json?.Value<string>("type")?.Split('.');
            if (requestTypeParts == null) {
                throw new ArgumentException("json");
            }

            var requestType = requestTypeParts.Length > 1 ? requestTypeParts[0] : string.Empty;
            var requestSubtype = requestTypeParts.Last();

            var request = Parse(requestType, requestSubtype, json);
            if (request == null) {
                throw new ArgumentException("json");
            }

            return request;
        }

        public void AddInterface(string name, Func<string, JObject, SpeechletRequest> resolver) {
            resolvers[name] = resolver;
        }
    }
}