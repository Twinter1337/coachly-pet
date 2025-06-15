namespace CoachlyBackEnd.Models;

public partial class Specialization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TrainerSpecialization> TrainerSpecializations { get; set; } = new List<TrainerSpecialization>();
}
