using Speakr.TalksApi.DataAccess.DbAccess;
using System.Linq;
using Speakr.TalksApi.Models.FeedbackForm;
using System.Collections.Generic;
using Newtonsoft.Json;
using Speakr.TalksApi.Models.Talks;
using System;

namespace Speakr.TalksApi.DataAccess
{
    public class Repository : IRepository
    {
        private IDapper _dapper;

        public Repository(IDapper dapper)
        {
            _dapper = dapper;
        }

        public string VerifyConnection()
        {
            return _dapper.Query<string>("SELECT '1'").FirstOrDefault();
        }

        public int InsertQuestionnaire(IList<Question> questionnaire)
        {
            var serializedQuestionnaire = JsonConvert.SerializeObject(questionnaire);
            var query = @"INSERT INTO `Questionnaires` (Questionnaire) 
                          VALUES (@serializedQuestionnaire);
                          SELECT LAST_INSERT_ID();";
            return _dapper.Query<int>(query, new {serializedQuestionnaire}).FirstOrDefault();
        }

        public int InsertTalk(TalkDTO talkDTO)
        {
            var query = @"
                INSERT INTO `Talks` 
                (TalkEasyAccessKey,TalkName,Topic,
                Description,SpeakerName,TalkCreationTime,
                TalkStartTime,QuestionnaireId)
                VALUES 
                (@TalkEasyAccessKey,@TalkName,@TalkTopic,
                @Description,@SpeakerName,@TalkCreationTime,
                @TalkStartTime,@QuestionnaireId);
                SELECT LAST_INSERT_ID()";
            return _dapper.Query<int>(query, talkDTO).FirstOrDefault();
        }

        public TalkDTO GetTalkById(int talkId)
        {
            var query = @"SELECT * FROM `talks` WHERE `TalkID` = @talkId";
            return _dapper.Query<TalkDTO>(query, new {talkId}).FirstOrDefault();
        }

        public TalkDTO GetTalkByEasyAccessKey(string easyAccessKey)
        {
            var query = @"SELECT * FROM `talks` WHERE `TalkEasyAccessKey` = @easyAccessKey";
            return _dapper.Query<TalkDTO>(query, new {easyAccessKey}).FirstOrDefault();
        }

        public FeedbackForm GetFeedbackForm(int questionnaireId)
        {
            var query = @"SELECT `Questionnaire` FROM `questionnaires` WHERE `QuestionnaireId` = @questionnaireId";
            return _dapper.Query<FeedbackForm>(query, new { questionnaireId }).FirstOrDefault();
        }
    }
}
