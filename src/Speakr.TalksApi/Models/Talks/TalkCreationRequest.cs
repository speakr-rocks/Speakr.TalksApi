using System;

namespace Speakr.TalksApi.Models.Talks
{
    public class TalkCreationRequest
    {
        public string EasyAccessKey { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string SpeakerName { get; set; }
        public DateTime TalkStartTime { get; set; }
    }
}
