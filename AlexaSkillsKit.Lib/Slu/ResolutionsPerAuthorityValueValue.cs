using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Slu
{
    public class ResolutionsPerAuthorityValueValue
    {
        public static ResolutionsPerAuthorityValueValue FromJson(JObject json) {
            if (json == null) return null;

            return new ResolutionsPerAuthorityValueValue {
                Name = json.Value<string>("name"),
                Id = json.Value<string>("id")
            };
        }

        public virtual string Name {
            get;
            set;
        }

        public virtual string Id {
            get;
            set;
        }
    }
}