using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.Payment;

public class PaymentRequestDto
{
    public decimal Amount { get; set; }
    public CurrencyType Currency { get; set; }
    public int UserId { get; set; }
    public int? SessionId { get; set; }
    public int? SubscriptionId { get; set; }
}