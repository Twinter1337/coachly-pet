using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.Payment;

public class PaymentDto
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }
    
    public PaymentMethod Method { get; set; }
    
    public PaymentStatus Status { get; set; }
    
    public CurrencyType Currency { get; set; }
}