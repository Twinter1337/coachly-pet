using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.Location;
using CoachlyBackEnd.Services.CRUD.Interfaces;

namespace CoachlyWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ICrudService<Location> _locationService;
    private readonly IMapper _mapper;

    public LocationController(ICrudService<Location> locationCrudService, IMapper mapper)
    {
        _locationService = locationCrudService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDto>>> GetAllLocations()
    {
        try
        {
            var locations = await _locationService.GetAllEntitiesAsync();

            return Ok(_mapper.Map<IEnumerable<LocationDto>>(locations));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDto>> GetLocation(int id)
    {
        try
        {
            var location = await _locationService.GetEntityByIdAsync(id);

            if (location == null)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<LocationDto>(location));
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Error retrieving Locations: {e.Message}");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLocation(int id, LocationDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            if (id != dto.Id)
            {
                return BadRequest("Ids must match");
            }

            return await _locationService.UpdateEntityAsync(id, dto)
                ? NoContent()
                : NotFound($"Location with ID {id} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error updating location: {ex.Message}");
        }
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchLocation(int id, LocationUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            return await _locationService.PatchEntityAsync(id, dto)
                ? NoContent()
                : NotFound($"Location with ID {id} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error patching location: {ex.Message}");
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<LocationDto>> PostLocation(LocationCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var location = _mapper.Map<Location>(dto);
            return await _locationService.CreateEntityAsync(location)
                ? CreatedAtAction(nameof(GetLocation), new { id = location.Id }, _mapper.Map<LocationDto>(location))
                : BadRequest("Failed to create location.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error creating location: {ex.Message}");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        try
        {
            return await _locationService.DeleteEntityAsync(id)
                ? NoContent()
                : NotFound($"Location with ID {id} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error deleting location: {ex.Message}");
        }
    }
}