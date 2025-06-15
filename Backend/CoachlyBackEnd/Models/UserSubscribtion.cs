namespace CoachlyBackEnd.Models;

public partial class UserSubscribtion
{
    public int Id { get; set; }

    public int? SubscribtionId { get; set; }

    public int? UserId { get; set; }

    public virtual Subscribtion? Subscribtion { get; set; }

    public virtual User? User { get; set; }
}
