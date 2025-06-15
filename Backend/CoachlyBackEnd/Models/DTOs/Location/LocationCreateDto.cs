using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.Location;

public class LocationCreateDto
{
    [Required]
    public string Country { get; set; } = null!;

    [Required]
    public string City { get; set; } = null!;

    [Required]
    public string Street { get; set; } = null!;

    [Required]
    public string BuildingNumber { get; set; } = null!;

    [Required]
    public string GymName { get; set; } = null!;
}