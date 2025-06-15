namespace CoachlyBackEnd.Models;

public partial class Trainer
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Bio { get; set; } = null!;

    public decimal AvgRating { get; set; }

    public int? LocationId { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Subscribtion> Subscribtions { get; set; } = new List<Subscribtion>();

    public virtual ICollection<TrainerAvailability> TrainerAvailabilities { get; set; } = new List<TrainerAvailability>();

    public virtual ICollection<TrainerDocument> TrainerDocuments { get; set; } = new List<TrainerDocument>();

    public virtual ICollection<TrainerSpecialization> TrainerSpecializations { get; set; } = new List<TrainerSpecialization>();

    public virtual User? User { get; set; }
}
