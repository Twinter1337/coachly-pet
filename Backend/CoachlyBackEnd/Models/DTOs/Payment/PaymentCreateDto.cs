using System.ComponentModel.DataAnnotations;
using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.Payment;

public class PaymentCreateDto
{
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public PaymentMethod Method { get; set; }
    
    [Required]
    public CurrencyType Currency { get; set; }
    
    [Required]
    public  string StripePaymentId { get; set; }
}