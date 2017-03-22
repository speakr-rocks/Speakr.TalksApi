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
        [SwaggerSummary("Not Implemented Yet")]
        public IActionResult GetReviewsByTalkId()
        {
            //return all reviews for a specific talk
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{talkId}/Reviews/{reviewId}")]
        [SwaggerSummary("Not Implemented Yet")]
        public IActionResult GetReviewById()
        {
            //return specific review for this talk - by review ID
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{talkId}/Reviews")]
        [SwaggerSummary("Saves a review response linked to the talk key provided")]
        [SwaggerNotes("Url: /talks/{talkId}/Reviews")]
        public IActionResult PostReviewForTalk(int talkId, [FromBody]FeedbackResponse _request)
        {
            if (talkId == 0)
                return StatusCode(409, "Invalid talk Id");

            if (talkId != _request.TalkId)
                return StatusCode(409, "Talk Id does not match the post body");

            if (_dbRepository.CheckTalkIdExists(talkId))
            {
                var reviewEntity = new ReviewEntity
                {
                    TalkId = talkId,
                    Answers = _request.Questionnaire,
                    SubmissionTime = _request.SubmissionTime
                };

                int reviewId = _dbRepository.InsertReview(reviewEntity);
                return CreatedAtAction("GetReviewById", "Reviews", new { TalkId = talkId, ReviewId = reviewId }, "Feedback Response Successfully Saved");
            }

            return StatusCode(409, "TalkId could not be found");

        }
    }
}
