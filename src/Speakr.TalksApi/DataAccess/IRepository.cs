using System.Collections.Generic;
using Speakr.TalksApi.Models.FeedbackForm;
using Speakr.TalksApi.Models.Talks;

namespace Speakr.TalksApi.DataAccess
{
    public interface IRepository
    {
        string VerifyConnection();
        int InsertQuestionnaire(IList<Question> defaultQuestionnaire);
        int InsertTalk(TalkEntity talkDTO);
        TalkEntity GetTalkById(int talkId);
        TalkEntity GetTalkByEasyAccessKey(string easyAccessKey);
        List<Question> GetQuestionnaire(int talkId);
    }
}
