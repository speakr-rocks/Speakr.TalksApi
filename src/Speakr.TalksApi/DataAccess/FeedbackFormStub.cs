using Speakr.TalksApi.Models.FeedbackForm;
using System.Collections.Generic;

namespace Speakr.TalksApi.DataAccess
{
    public static class FeedbackFormStub
    {
        public static FeedbackForm GetTalkById(string talkId)
        {
            var questionList = new List<Question>
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

            return new FeedbackForm()
            {
                TalkId = talkId,
                TalkName = "My First Talk",
                SpeakerId = "guid_speaker_id",
                SpeakerName = "J-Wow",
                Questionnaire = questionList
            };
        }

        //public static FeedbackResponse ReviewFormResponseModelMock(string talkId)
        //{
        //var model = GetTalkById(talkId);

        //var response = new FeedbackResponse()
        //{
        //    Questionnaire = model.Questionnaire,
        //    ReviewerId = "",
        //    SubmissionTime = DateTime.Now
        //};

        //return response;
        //}
    }
}
