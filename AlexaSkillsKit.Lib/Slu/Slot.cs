// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace AlexaSkillsKit.Slu
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-types-reference.html#slot-object
    /// </summary>
    public class Slot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Slot FromJson(JObject json) {
            if (json == null) return null;

            ConfirmationStatusEnum confirmationStatus;
            Enum.TryParse(json.Value<string>("confirmationStatus"), out confirmationStatus);
            return new Slot {
                Name = json.Value<string>("name"),
                Value = json.Value<string>("value"),
                ConfirmationStatus = confirmationStatus,
                Resolutions = Resolutions.FromJson(json.Value<JObject>("resolutions"))
            };
        }
        
        public virtual string Name {
            get;
            set;
        }

        public virtual string Value {
            get;
            set;
        }

        public virtual ConfirmationStatusEnum ConfirmationStatus {
            get;
            set;
        }

        [JsonIgnore]
        public virtual Resolutions Resolutions {
            get;
            set;
        }
    }
}