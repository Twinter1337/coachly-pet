namespace CoachlyBackEnd.Models.DTOs.Review;

public class ReviewDto
{
    public int Id { get; set; }
    
    public int TrainerId { get; set; }

    public int UserId { get; set; }

    public decimal Rating { get; set; }

    public string Text { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}