using Speakr.TalksApi.Models.FeedbackForm;
using System.Collections.Generic;

namespace Speakr.TalksApi.DataAccess.DataObjects
{
    public class ReviewEntity
    {
        public int TalkId;
        public IList<Question> Answers;
    }
}
