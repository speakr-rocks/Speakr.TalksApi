using System.Collections.Generic;

namespace Speakr.TalksApi.Models.FeedbackForm
{
    public class FeedbackForm
    {
        public int TalkId { get; set; }
        public string EasyAccessKey { get; set; }
        public string TalkName { get; set; }
        public string Description { get; set; }
        public string SpeakerName { get; set; }
        public IList<Question> Questionnaire { get; set; }
    }
}