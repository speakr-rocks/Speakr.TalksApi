using Speakr.TalksApi.DataAccess.Templates;
using Speakr.TalksApi.Models.FeedbackForm;

namespace Speakr.TalksApi.DataAccess
{
    public static class FeedbackFormStub
    {
        public static FeedbackForm GetTalkById(string talkId)
        {
            return new FeedbackForm()
            {
                TalkId = talkId,
                TalkName = "My First Talk",
                SpeakerId = "guid_speaker_id",
                SpeakerName = "J-Wow",
                Questionnaire = DefaultQuestionnaire.GetDefaultQuestionnaire()
            };
        }
    }
}