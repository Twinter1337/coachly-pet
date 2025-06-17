using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models;

public partial class Payment
{
    public int Id { get; set; }
    
    public string? StripePaymentId { get; set; }

    public decimal Amount { get; set; }
    
    public int? UserId { get; set; }

    public DateTime PaymentDate { get; set; }
    
    public PaymentStatus Status { get; set; }
    
    public PaymentMethod Method { get; set; }
    
    public CurrencyType Currency { get; set; }
    
    public virtual User? User { get; set; }

    public virtual Session? Session { get; set; }

    public virtual Subscribtion? Subscribtion { get; set; }
}
