using Microsoft.AspNetCore.Mvc;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DataObjects;
using Speakr.TalksApi.Models.FeedbackForm;
using System;
using Speakr.TalksApi.Swagger;

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
        [SwaggerSummary("Saves a review response linked to the talk key provided")]
        [SwaggerNotes("Url: /talks/{easyAccessKey}/Reviews")]
        public IActionResult PostReviewForTalk(string easyAccessKey, [FromBody]FeedbackResponse _request)
        {
            int talkId = _dbRepository.GetTalkIdFromEasyAccessKey(easyAccessKey);

            if (talkId == 0 || talkId != _request.TalkId)
                return StatusCode(409, "TalkId could not be found");

            var reviewEntity = new ReviewEntity
            {
                TalkId = talkId,
                Answers = _request.Questionnaire,
                SubmissionTime = _request.SubmissionTime
            };

            int reviewId = _dbRepository.InsertReview(reviewEntity);
            return CreatedAtAction("GetReviewById", "Reviews", new { TalkId = talkId, ReviewId = reviewId }, "Feedback Response Successfully Saved");
        }
    }
}
