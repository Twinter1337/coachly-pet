namespace CoachlyBackEnd.Models.DTOs.Location;

public class LocationDto
{
    public int Id { get; set; }
    
    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string BuildingNumber { get; set; } = null!;

    public string GymName { get; set; } = null!;
}