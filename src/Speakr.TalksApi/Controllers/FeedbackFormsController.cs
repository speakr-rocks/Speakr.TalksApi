using Microsoft.AspNetCore.Mvc;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.Models.FeedbackForm;
using System.Threading.Tasks;
using Speakr.TalksApi.Swagger;

namespace Speakr.TalksApi.Controllers
{
    [Route("")]
    public class FeedbackFormController : Controller
    {
        private readonly IRepository _dbRepository;

        public FeedbackFormController(IRepository repository)
        {
            _dbRepository = repository;
        }

        [HttpGet]
        [Produces(typeof(FeedbackForm))]
        [Route("Feedbackform")]
        [SwaggerSummary("Get FeedbackForm by easy access key (string)")]
        [SwaggerNotes("Url: /feedbackform?key={easyAccessKey}")]
        public async Task<IActionResult> GetFormBykey([FromQuery] string key)
        {
            var talk = _dbRepository.GetTalkByEasyAccessKey(key);

            if (talk == null)
                return NotFound($"Could not find talk with easy access key: {key}");

            var questionnaire = _dbRepository.GetQuestionnaire(talk.Id);

            if(questionnaire == null)
                return NotFound($"Could not find feedback form with talk id key: {talk.Id}");

            var feedbackForm = new FeedbackForm();

            feedbackForm.TalkId = talk.Id;
            feedbackForm.TalkName = talk.Name;
            feedbackForm.SpeakerName = talk.SpeakerName;
            feedbackForm.EasyAccessKey = talk.EasyAccessKey;
            feedbackForm.Questionnaire = questionnaire;
            feedbackForm.Description = talk.Description;

            return Ok(feedbackForm);
        }

        // /talks/{talkId}/FeedbackForm should be to EDIT/UPDATE feedback forms
        // Add that controller action here
        // These controllers need to be authenticated (talk owner ONLY!)
    }
}
