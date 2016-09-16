using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Models.FeedbackForm
{
    public class Question
    {
        public string QuestionId { get; set; }
        public bool IsRequired { get; set; }
        public string QuestionText { get; set; }
        public AnswerTypes ResponseType { get; set; }
        public string Answer { get; set; }
    }
}
