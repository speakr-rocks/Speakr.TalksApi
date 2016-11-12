using Speakr.TalksApi.DataAccess.DbAccess;
using System.Linq;
using Speakr.TalksApi.Models.FeedbackForm;
using System.Collections.Generic;
using Newtonsoft.Json;
using Speakr.TalksApi.DataAccess.DataObjects;
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

        public int InsertTalk(TalkEntity talkDTO)
        {
            var query = @"
                INSERT INTO `Talks` 
                (EasyAccessKey,Name,Topic,
                Description,SpeakerName,TalkCreationTime,
                TalkStartTime,QuestionnaireId)
                VALUES 
                (@EasyAccessKey,@Name,@Topic,
                @Description,@SpeakerName,@TalkCreationTime,
                @TalkStartTime,@QuestionnaireId);
                SELECT LAST_INSERT_ID()";
            return _dapper.Query<int>(query, talkDTO).FirstOrDefault();
        }

        public int InsertReview(ReviewEntity feedbackEntity)
        {
            var deserializedAnswers = JsonConvert.SerializeObject(feedbackEntity.Answers);

            var query = @"
                INSERT INTO `Feedback` 
                (TalkId,Answer)
                VALUES 
                (@TalkId,@Answers);
                SELECT LAST_INSERT_ID()";

            return _dapper.Query<int>(query, new { TalkId = feedbackEntity.TalkId, Answers = deserializedAnswers }).FirstOrDefault();
        }

        public TalkEntity GetTalkById(int talkId)
        {
            var query = @"SELECT * FROM `Talks` WHERE `Id` = @talkId";
            return _dapper.Query<TalkEntity>(query, new {talkId}).FirstOrDefault();
        }

        public TalkEntity GetTalkByEasyAccessKey(string easyAccessKey)
        {
            var query = @"SELECT * FROM `Talks` WHERE `EasyAccessKey` = @easyAccessKey";
            return _dapper.Query<TalkEntity>(query, new {easyAccessKey}).FirstOrDefault();
        }

        public List<Question> GetQuestionnaire(int questionnaireId)
        {
            var query = @"SELECT `Questionnaire` 
                          FROM `Questionnaires` 
                          WHERE `Id` = @questionnaireId";
            var questionnaire = _dapper.Query<string>(query, new { questionnaireId }).FirstOrDefault();

            if (questionnaire == null)
                return null;

            return JsonConvert.DeserializeObject<List<Question>>(questionnaire);
        }

        public int GetTalkIdFromEasyAccessKey(string easyAccessKey)
        {
            var query = @"SELECT Id FROM `Talks` WHERE `EasyAccessKey` = @easyAccessKey;";
            return _dapper.Query<int>(query, new { easyAccessKey }).FirstOrDefault();
        }
    }
}
