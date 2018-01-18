// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;
using System;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#playbackfailed
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#system-exceptionencountered
    /// </summary>
    public class Error
    {
        public static Error FromJson(JObject json) {
            if (json == null) return null;

            TypeEnum type = TypeEnum.NONE;
            Enum.TryParse(json.Value<string>("type"), out type);
            return new Error {
                Type = type,
                Message = json.Value<string>("message")
            };
        }

        public TypeEnum Type {
            get;
            private set;
        }

        public string Message {
            get;
            private set;
        }

        public enum TypeEnum
        {
            NONE = 0, // default in case parsing fails
            INVALID_RESPONSE,
            DEVICE_COMMUNICATION_ERROR,
            INTERNAL_ERROR,
            MEDIA_ERROR_UNKNOWN,
            MEDIA_ERROR_INVALID_REQUEST,
            MEDIA_ERROR_SERVICE_UNAVAILABLE,
            MEDIA_ERROR_INTERNAL_SERVER_ERROR,
            MEDIA_ERROR_INTERNAL_DEVICE_ERROR
        }
    }
}