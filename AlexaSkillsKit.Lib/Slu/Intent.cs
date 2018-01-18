// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System;

namespace AlexaSkillsKit.Slu
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-types-reference.html#intent-object
    /// </summary>
    public class Intent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Intent FromJson(JObject json) {
            if (json == null) return null;

            var slots = json.Value<JObject>("slots")?.Children<JProperty>()
                .ToDictionary(x => x.Name, x => Slot.FromJson(x.Value as JObject));
            ConfirmationStatusEnum confirmationStatus;
            Enum.TryParse(json.Value<string>("confirmationStatus"), out confirmationStatus);

            return new Intent {
                Name = json.Value<string>("name"),
                ConfirmationStatus = confirmationStatus,
                Slots = slots ?? new Dictionary<string, Slot>()
            };
        }

        public virtual string Name {
            get;
            set;
        }

        public virtual ConfirmationStatusEnum ConfirmationStatus {
            get;
            set;
        }

        public virtual IDictionary<string, Slot> Slots {
            get;
            set;
        }
    }
}