using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.Subscribtion;

public class SubscribtionCreateDto
{
    [Required]
    public int TrainerId { get; set; }

    [Required]
    public DateTime ValidityPeriod { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int? PaymentId { get; set; }

    [Required]
    public string Conditions { get; set; } = null!;
}