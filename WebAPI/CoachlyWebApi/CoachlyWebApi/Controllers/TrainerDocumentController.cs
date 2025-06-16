using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.TrainerDocument;
using CoachlyBackEnd.Services.CRUD.Interfaces;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerDocumentController : ControllerBase
    {
        private readonly ICrudService<TrainerDocument> _trainerDocumentService;
        private readonly IMapper _mapper;

        public TrainerDocumentController(ICrudService<TrainerDocument> trainerDocumentService, IMapper mapper)
        {
            _trainerDocumentService = trainerDocumentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDocumentDto>>> GetAllTrainerDocuments()
        {
            try
            {
                var trainerDocuments = await _trainerDocumentService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<TrainerDocumentDto>>(trainerDocuments));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerDocumentDto>> GetTrainerDocument(int id)
        {
            try
            {
                var trainerDocument = await _trainerDocumentService.GetEntityByIdAsync(id);

                if (trainerDocument == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<TrainerDocumentDto>(trainerDocument));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error retrieving Locations: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainerDocument(int id, TrainerDocumentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id != dto.Id)
                {
                    return BadRequest("Ids must match");
                }

                return await _trainerDocumentService.UpdateEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Trainer document with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating Trainer document: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTrainerDocument(int id, TrainerDocumentUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _trainerDocumentService.PatchEntityAsync(id, dto)
                    ? NoContent()
                    : NotFound($"Trainer document with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching trainer document: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TrainerDocumentDto>> PostTrainerDocument(TrainerDocumentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var trainerDocument = _mapper.Map<TrainerDocument>(dto);
                return await _trainerDocumentService.CreateEntityAsync(trainerDocument)
                    ? CreatedAtAction(nameof(GetTrainerDocument), new { id = trainerDocument.Id },
                        _mapper.Map<TrainerDocumentDto>(trainerDocument))
                    : BadRequest("Failed to create trainer document.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating trainer document: {ex.Message}");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainerDocument(int id)
        {
            try
            {
                return await _trainerDocumentService.DeleteEntityAsync(id)
                    ? NoContent()
                    : NotFound($"Trainer document with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting trainer document: {ex.Message}");
            }
        }
    }
}