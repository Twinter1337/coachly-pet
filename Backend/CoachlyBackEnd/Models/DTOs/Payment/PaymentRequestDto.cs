using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.Payment;

public class PaymentRequestDto
{
    public decimal Amount { get; set; }
    public CurrencyType Currency { get; set; }
    public string SuccessUrl { get; set; }
    public string CancelUrl { get; set; }
}