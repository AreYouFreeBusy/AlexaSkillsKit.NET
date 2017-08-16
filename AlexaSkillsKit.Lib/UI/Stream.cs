namespace AlexaSkillsKit.UI
{
    public class Stream
    {
        public virtual string Token
        {
            get;
            set;
        }

        public virtual string Url
        {
            get;
            set;
        }

        public virtual int OffsetInMilliseconds
        {
            get;
            set;
        }
    }
}
