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

            var talkId = talkForm.Id;
            var form = _dbRepository.GetFeedbackForm(talkId.Value);

            if (form == null)
                return NotFound();

            return Ok(form);
        }
    }
}
