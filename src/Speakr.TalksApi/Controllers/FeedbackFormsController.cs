using Microsoft.AspNetCore.Mvc;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.Models.FeedbackForm;
using System.Threading.Tasks;


namespace Speakr.TalksApi.Controllers
{
    [Route("talks")]
    public class FeedbackFormsController : Controller
    {
        [HttpGet]
        [Route("{talkId}/feedbackform")]
        [Produces(typeof(FeedbackForm))]
        public async Task<IActionResult> GetFeedbackFormsAsync(string talkId)
        {
            if (talkId.Equals("abcde"))
                return NotFound();

            return Ok(FeedbackFormStub.GetTalkById(talkId));
        }
    }
}
