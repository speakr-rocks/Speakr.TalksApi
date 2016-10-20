using Speakr.TalksApi.Models.FeedbackForm;
using System.Collections.Generic;

namespace Speakr.TalksApi.DataAccess.Templates
{
    public static class DefaultQuestionnaire
    {
        public static IList<Question> GetDefaultQuestionnaire()
        {
            return new List<Question>
            {
                new Question
                {
                    QuestionId = "Question-1",
                    QuestionText = "How much did you enjoy the talk?",
                    Answer = "",
                    ResponseType = AnswerTypes.Emoji,
                    IsRequired = true
                },

                new Question
                {
                    QuestionId = "Question-2",
                    QuestionText = "How would you rate this talk?",
                    Answer = "",
                    ResponseType = AnswerTypes.Rating,
                    IsRequired = false
                },

                new Question
                {
                    QuestionId = "Question-3",
                    QuestionText = "Did you learn anything useful?",
                    Answer = "",
                    ResponseType = AnswerTypes.YesNo,
                    IsRequired = true
                },

                new Question
                {
                    QuestionId = "Question-4",
                    QuestionText = "Would you recommend this talk to a friend/colleague?",
                    Answer = "",
                    ResponseType = AnswerTypes.YesNo,
                    IsRequired = false
                },

                new Question
                {
                    QuestionId = "Question-5",
                    QuestionText = "Do you have any suggestions to improve this talk?",
                    Answer = "",
                    ResponseType = AnswerTypes.Text,
                    IsRequired = true
                },

                new Question
                {
                    QuestionId = "Question-6",
                    QuestionText = "Any other comments?",
                    Answer = "",
                    ResponseType = AnswerTypes.Text,
                    IsRequired = false
                }
            };
        }
    }
}
