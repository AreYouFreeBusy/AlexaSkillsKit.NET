using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public class AudioPlayerRequest : SpeechletRequest
    {
        public AudioPlayerRequest(string requestId, DateTime timestamp, string token, long offsetInMilliseconds, string type) : base(requestId, timestamp)
        {
            Token = token;
            OffsetInMilliseconds = offsetInMilliseconds;
            Type = type;
        }
        
        public string Token { get; set; }
        public long OffsetInMilliseconds { get; set; }
        public string Type { get; set; }
    }
}
