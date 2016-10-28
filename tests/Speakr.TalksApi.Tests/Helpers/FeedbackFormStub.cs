using Newtonsoft.Json;
using Speakr.TalksApi.DataAccess.Templates;
using Speakr.TalksApi.Models.FeedbackForm;
using System.Collections.Generic;

namespace Speakr.TalksApi.Tests.Helpers
{
    public static class FeedbackFormStub
    {
        public static FeedbackForm GetTalkById(int talkId, string talkEasyAccessKey)
        {
            var talk = TalkEntityStub.GetTalk(talkId, talkEasyAccessKey);

            return new FeedbackForm()
            {
                TalkId = talk.Id,
                EasyAccessKey = talk.EasyAccessKey,
                TalkName = talk.Name,
                SpeakerName = talk.SpeakerName,
                Questionnaire = GetQuestionnaire()
            };
        }

        public static IList<Question> GetQuestionnaire()
        {
            return DefaultQuestionnaire.GetDefaultQuestionnaire();
        }

        public static string GetQuestionnaireAsJson()
        {
            return JsonConvert.SerializeObject(GetQuestionnaire());
        }
    }
}