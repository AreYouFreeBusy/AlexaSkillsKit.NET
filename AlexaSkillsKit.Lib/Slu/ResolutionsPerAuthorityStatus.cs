using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Slu
{
    public class ResolutionsPerAuthorityStatus
    {
        public static ResolutionsPerAuthorityStatus FromJson(JObject json) {
            if (json == null) return null;

            return new ResolutionsPerAuthorityStatus {
                Code = json.Value<string>("code")
            };
        }

        public virtual string Code {
            get;
            set;
        }
    }
}