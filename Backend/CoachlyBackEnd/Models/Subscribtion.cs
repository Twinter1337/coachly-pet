namespace CoachlyBackEnd.Models;

public partial class Subscribtion
{
    public int Id { get; set; }

    public int? TrainerId { get; set; }

    public TimeSpan ValidityPeriod { get; set; }

    public decimal Price { get; set; }

    public int? PaymentId { get; set; }

    public string Conditions { get; set; } = null!;

    public virtual Payment? Payment { get; set; }

    public virtual Trainer? Trainer { get; set; }

    public virtual ICollection<UserSubscribtion> UserSubscribtions { get; set; } = new List<UserSubscribtion>();
}
