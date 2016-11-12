using Microsoft.AspNetCore.Mvc;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DataObjects;
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

        [HttpGet]
        [Route("")]
        [Produces(typeof(TalkEntity))]
        [SwaggerSummary("Get Talk Information by Id (int)")]
        [SwaggerNotes("Url: /talks?talkId={talkId}")]
        public async Task<IActionResult> GetTalkById([FromQuery] int talkId)
        {
            var talkDTO = _dbRepository.GetTalkById(talkId);

            if(talkDTO == null)
                return NotFound();

            return Ok(talkDTO);
        }

        [HttpPost]
        [Route("")]
        [SwaggerSummary("Create a new Talk, Generates default questionnaire")]
        [SwaggerNotes("Url: /talks/")]
        public async Task<IActionResult> PostTalk([FromBody]TalkCreationRequest request)
        {
            var talkDTO = CreateNewTalk(request);
            var talkId = _dbRepository.InsertTalk(talkDTO);
            return CreatedAtAction("GetTalkById", "?talkId=", talkId);
        }

        private TalkEntity CreateNewTalk(TalkCreationRequest request)
        {
            var defaultQuestionnaire = DefaultQuestionnaire.GetDefaultQuestionnaire();
            var questionnaireId = _dbRepository.InsertQuestionnaire(defaultQuestionnaire);

            var talkDTO = new TalkEntity
            {
                Name = request.Name,
                EasyAccessKey = request.EasyAccessKey,
                Topic = request.Topic,
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
