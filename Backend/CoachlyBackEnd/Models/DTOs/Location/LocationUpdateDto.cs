namespace CoachlyBackEnd.Models.DTOs.Location;

public class LocationUpdateDto
{
    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? BuildingNumber { get; set; } 

    public string? GymName { get; set; }
}