using Speakr.TalksApi.DataAccess.DataObjects;

namespace Speakr.TalksApi.Tests.Helpers
{
    public static class TalkEntityBuilder
    {
        public static TalkEntity BuildTalkEntityById(int id, string easyaccesskey)
        {
            return new TalkEntity
            {
                Id = id,
                EasyAccessKey = easyaccesskey,
                SpeakerName = "SpeakerName",
                Description = "Description",
                Name = "TalkName",
                Topic = "TalkTopic",
                QuestionnaireId = 1
            };
        }
    }
}
