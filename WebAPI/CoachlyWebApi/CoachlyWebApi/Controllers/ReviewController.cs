using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.Review;
using CoachlyBackEnd.Services.CRUD.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ICrudService<Review> _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(ICrudService<Review> reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviews));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            try
            {
                var review = await _reviewService.GetEntityByIdAsync(id);

                if (review == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ReviewDto>(review));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error retrieving reviews: {e.Message}");
            }
        }

        [Authorize(Roles = "Admin, Client")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id != dto.Id)
                {
                    return BadRequest("Ids must match");
                }

                return await _reviewService.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Review with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating review: {ex.Message}");
            }
        }
        
        [Authorize(Roles = "Admin, Client")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchReview(int id, ReviewUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _reviewService.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Review with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching review: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Client")]
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> PostReview(ReviewCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var review = _mapper.Map<Review>(dto);
                return await _reviewService.CreateEntityAsync(review)
                    ? CreatedAtAction(nameof(GetReview), new { id = review.Id }, _mapper.Map<ReviewDto>(review))
                    : BadRequest("Failed to create review.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating review: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Client")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                return await _reviewService.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"Review with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting review: {ex.Message}");
            }
        }
    }
}