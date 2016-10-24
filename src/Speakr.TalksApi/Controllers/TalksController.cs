using Microsoft.AspNetCore.Mvc;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.Templates;
using Speakr.TalksApi.Models.Talks;
using System;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Controllers
{
    [Route("talks")]
    public class TalksController : Controller
    {
        private IRepository _dbRepository;

        public TalksController(IRepository repository)
        {
            _dbRepository = repository;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetTalkByEasyAccessKey(string easyAccessKey)
        {
            return Ok();
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetTalkById(int talkId)
        {
            var expectedDto = new TalkDTO
            {
                Id = talkId,
                TalkEasyAccessKey = "Clever_Einstein",
                TalkName = "Talk 101",
            };

            return Ok(expectedDto);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> PostTalk([FromBody]TalkCreationRequest request)
        {
            var talkDTO = CreateNewTalk(request);
            var easyAccessTalkKey = _dbRepository.InsertTalk(talkDTO);
            return CreatedAtRoute("GetTalkById", easyAccessTalkKey);
        }

        private TalkDTO CreateNewTalk(TalkCreationRequest request)
        {
            var defaultQuestionnaire = DefaultQuestionnaire.GetDefaultQuestionnaire();
            var questionnaireId = _dbRepository.InsertQuestionnaire(defaultQuestionnaire);

            var talkDTO = new TalkDTO
            {
                TalkName = request.TalkName,
                TalkEasyAccessKey = request.TalkEasyAccessKey,
                TalkTopic = request.TalkTopic,
                Description = request.Description,
                SpeakerName = request.SpeakerName,
                TalkCreationTime = DateTime.Now,
                TalkStartTime = request.TalkStartTime,
                QuestionnaireId = questionnaireId
            };

            return talkDTO;
        }
    }
}
