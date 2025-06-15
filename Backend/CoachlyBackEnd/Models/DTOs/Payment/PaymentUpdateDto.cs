using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.Payment;

public class PaymentUpdateDto
{
    public decimal? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }
    
    public PaymentMethod? Method { get; set; }
    
    public PaymentStatus? Status { get; set; }
}