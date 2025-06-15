namespace CoachlyBackEnd.Models.DTOs.TrainerDtos;

public class TrainerDto
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Bio { get; set; } = null!;

    public decimal AvgRating { get; set; }

    public int? LocationId { get; set; }
}