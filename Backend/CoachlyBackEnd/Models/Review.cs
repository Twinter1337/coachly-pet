namespace CoachlyBackEnd.Models;

public partial class Review
{
    public int Id { get; set; }

    public int? TrainerId { get; set; }

    public int? UserId { get; set; }

    public decimal Rating { get; set; }

    public string Text { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Trainer? Trainer { get; set; }

    public virtual User? User { get; set; }
}
