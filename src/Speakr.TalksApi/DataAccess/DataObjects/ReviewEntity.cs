using Speakr.TalksApi.Models.FeedbackForm;
using System;
using System.Collections.Generic;

namespace Speakr.TalksApi.DataAccess.DataObjects
{
    public class ReviewEntity
    {
        public int Id;
        public int TalkId;
        public IList<Question> Answers;
        public DateTime SubmissionTime;
    }
}
