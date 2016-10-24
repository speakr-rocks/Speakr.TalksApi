using System.Collections.Generic;
using Speakr.TalksApi.Models.FeedbackForm;
using Speakr.TalksApi.Models.Talks;

namespace Speakr.TalksApi.DataAccess
{
    public interface IRepository
    {
        string VerifyConnection();
        int InsertQuestionnaire(IList<Question> defaultQuestionnaire);
        int InsertTalk(TalkDTO talkDTO);
        TalkDTO GetTalkById(int talkId);
        FeedbackForm GetFeedbackForm(string easyAccessKey);
    }
}
