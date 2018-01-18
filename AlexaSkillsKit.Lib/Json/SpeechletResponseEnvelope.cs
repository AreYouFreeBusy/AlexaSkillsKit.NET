// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System.Collections.Generic;
using Newtonsoft.Json;
using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Json
{
    public class SpeechletResponseEnvelope
    {
        private static JsonSerializerSettings _serializerSettings = new JsonSerializerSettings() {
            NullValueHandling = NullValueHandling.Ignore, 
            ContractResolver = new CamelCasePropertyNamesExceptDictionaryKeysContractResolver(),
            Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter() }
        };


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string ToJson() {
            return JsonConvert.SerializeObject(this, _serializerSettings);
        }


        public virtual ISpeechletResponse Response {
            get;
            set;
        }

        public virtual Dictionary<string, string> SessionAttributes {
            get;
            set;
        }

        public virtual string Version {
            get;
            set;
        }
    }
}