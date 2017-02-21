using Newtonsoft.Json;
using Speakr.TalksApi.DataAccess.Templates;
using Speakr.TalksApi.Models.FeedbackForm;
using System.Collections.Generic;

namespace Speakr.TalksApi.Tests.Helpers
{
    public static class FeedbackFormBuilder
    {
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