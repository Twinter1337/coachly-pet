namespace CoachlyBackEnd.Models.DTOs.Review;

public class ReviewUpdateDto
{
    public int? TrainerId { get; set; }

    public int? UserId { get; set; }

    public decimal? Rating { get; set; }

    public string? Text { get; set; }

    public DateTime? CreatedAt { get; set; }
}