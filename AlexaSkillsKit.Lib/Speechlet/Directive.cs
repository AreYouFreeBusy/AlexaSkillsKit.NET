namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-and-response-json-reference.html#response-object
    /// </summary>
    public class Directive
    {
        public Directive(string type) {
            Type = type;
        }

        public virtual string Type {
            get;
            private set;
        }
    }
}