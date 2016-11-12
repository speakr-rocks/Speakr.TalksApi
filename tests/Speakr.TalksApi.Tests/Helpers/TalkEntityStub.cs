using Speakr.TalksApi.DataAccess.DataObjects;

namespace Speakr.TalksApi.Tests.Helpers
{
    public static class TalkEntityStub
    {
        public static TalkEntity GetTalk(int id, string easyaccesskey)
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
