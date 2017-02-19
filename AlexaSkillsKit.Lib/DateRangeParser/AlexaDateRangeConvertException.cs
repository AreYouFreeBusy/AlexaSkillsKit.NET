using System;
using System.Runtime.Serialization;

namespace AlexaSkillsKit.DateRangeParser
{
    [Serializable]
    public class AlexaDateRangeConvertException : Exception
    {
        public AlexaDateRangeConvertException()
        {
        }

        public AlexaDateRangeConvertException(string message) : base(message)
        {
        }

        public AlexaDateRangeConvertException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlexaDateRangeConvertException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
