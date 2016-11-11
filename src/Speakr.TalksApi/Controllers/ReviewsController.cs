using Microsoft.AspNetCore.Mvc;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DataObjects;
using Speakr.TalksApi.Models.FeedbackForm;
using System;

namespace Speakr.TalksApi.Controllers
{
    [Route("talks")]
    public class ReviewsController : Controller
    {
        private IRepository _dbRepository;

        public ReviewsController(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        [HttpGet]
        [Route("{talkId}/Reviews")]
        public IActionResult GetReviewsByTalkId()
        {
            //return all reviews for a specific talk
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{talkId}/Reviews/{reviewId}")]
        public IActionResult GetReviewById()
        {
            //return specific ID
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{easyAccessKey}/Reviews")]
        [SwaggerSummary("Saves a feedback response linked to the talk key provided")]
        [SwaggerNotes("Url: /talks/{easyAccessKey}/Reviews")]
        public IActionResult PostReviewForTalk(string easyAccessKey, [FromBody]FeedbackResponse _request)
        {
            int talkId = _request.TalkId;

            var reviewEntity = new ReviewEntity
            {
                TalkId = talkId,
                Answers = _request.Questionnaire
            };

            int reviewId = _dbRepository.InsertReview(reviewEntity);
            return CreatedAtAction("GetReviewById", "Reviews", new { TalkId = talkId, ReviewId = reviewId }, "Feedback Response Successfully Saved");
        }
    }
}
