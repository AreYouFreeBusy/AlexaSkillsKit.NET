// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AlexaSkillsKit.Slu
{
    public class ResolutionsPerAuthority
    {
        public static ResolutionsPerAuthority FromJson(JObject json) {
            if (json == null) return null;

            var values = json.Value<JArray>("values")?.Children()
                .Select(x => ResolutionsPerAuthorityValue.FromJson(x.Value<JObject>()));
            return new ResolutionsPerAuthority {
                Authority = json.Value<string>("authority"),
                Status = ResolutionsPerAuthorityStatus.FromJson(json.Value<JObject>("status")),
                Values = values
            };
        }

        public virtual string Authority {
            get;
            set;
        }

        public virtual ResolutionsPerAuthorityStatus Status {
            get;
            set;
        }

        public virtual IEnumerable<ResolutionsPerAuthorityValue> Values {
            get;
            set;
        }
    }
}