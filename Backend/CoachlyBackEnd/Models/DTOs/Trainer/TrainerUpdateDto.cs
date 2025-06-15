using CoachlyBackEnd.Models.DTOs.Location;
using CoachlyBackEnd.Models.DTOs.UserDtos;

namespace CoachlyBackEnd.Models.DTOs.TrainerDtos;

public class TrainerUpdateDto
{
    public int? UserId { get; set; }

    public string? Bio { get; set; } = null!;

    public decimal? AvgRating { get; set; }

    public int? LocationId { get; set; }
}