namespace CoachlyBackEnd.Models;

public partial class TrainerSpecialization
{
    public int Id { get; set; }

    public int? TrainerId { get; set; }

    public int? SpecalizationId { get; set; }

    public virtual Specialization? Specalization { get; set; }

    public virtual Trainer? Trainer { get; set; }
}
