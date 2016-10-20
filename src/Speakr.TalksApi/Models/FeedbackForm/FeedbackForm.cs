using System.Collections.Generic;

namespace Speakr.TalksApi.Models.FeedbackForm
{
    public class FeedbackForm
    {
        public string TalkId { get; set; }
        public string TalkName { get; set; }
        public string SpeakerId { get; set; }
        public string SpeakerName { get; set; }
        public IList<Question> Questionnaire { get; set; }
    }
}