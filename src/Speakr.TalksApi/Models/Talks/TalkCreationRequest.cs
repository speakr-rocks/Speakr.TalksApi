using System;

namespace Speakr.TalksApi.Models.Talks
{
    public class TalkCreationRequest
    {
        public string TalkEasyAccessKey { get; set; }
        public string TalkName { get; set; }
        public string TalkTopic { get; set; }
        public string Description { get; set; }
        public string SpeakerName { get; set; }
        public DateTime TalkStartTime { get; set; }
    }
}
