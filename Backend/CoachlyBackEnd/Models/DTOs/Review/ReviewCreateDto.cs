using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.Review;

public class ReviewCreateDto
{
    [Required]
    public int TrainerId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public decimal Rating { get; set; }

    [Required]
    public string Text { get; set; } = null!;
}