using Microsoft.AspNetCore.Mvc;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.Models.FeedbackForm;
using System.Threading.Tasks;


namespace Speakr.TalksApi.Controllers
{
    [Route("talks")]
    public class FeedbackFormsController : Controller
    {
        private IRepository _dbRepository;

        public FeedbackFormsController(IRepository repository)
        {
            _dbRepository = repository;
        }

        [HttpGet]
        [Route("{easyAccessKey}/feedbackform")]
        [Produces(typeof(FeedbackForm))]
        public async Task<IActionResult> GetFeedbackFormsAsync(string easyAccessKey)
        {
            var talkForm = _dbRepository.GetTalkByEasyAccessKey(easyAccessKey);

            if (talkForm == null)
                return NotFound();

            //Pass the above talk into a mapper.
            //Mapper should create feedback form and populate initial fields
            //Below query should return a List<Questions> not a feedback form
            //That list of questions gets assigned to feedback form and returned to controller

            //var questionnaireId = talkForm.QuestionnaireId;
            //var form = _dbRepository.GetFeedbackForm(questionnaireId);

            //if (form == null)
            //    return NotFound();

            //return Ok(form);
            return Ok(new FeedbackForm());
        }
    }
}
