// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AlexaSkillsKit.Slu
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-types-reference.html#resolutions-object
    /// </summary>
    public class Resolutions
    {
        public static Resolutions FromJson(JObject json) {
            if (json == null) return null;

            var resolutionsPerAuthority = json.Value<JArray>("resolutionsPerAuthority")?.Children()
                .Select(x => Slu.ResolutionsPerAuthority.FromJson(x.Value<JObject>()));
            return new Resolutions {
                ResolutionsPerAuthority = resolutionsPerAuthority
            };
        }

        public virtual IEnumerable<ResolutionsPerAuthority> ResolutionsPerAuthority {
            get;
            set;
        }
    }
}