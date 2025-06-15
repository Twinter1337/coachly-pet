namespace CoachlyBackEnd.Models.DTOs.Subscribtion;

public class SubscribtionUpdateDto
{
    public int? TrainerId { get; set; }

    public DateTime? ValidityPeriod { get; set; }

    public decimal? Price { get; set; }

    public int? PaymentId { get; set; }

    public string? Conditions { get; set; } = null!;
}