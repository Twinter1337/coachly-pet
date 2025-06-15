namespace CoachlyBackEnd.Models;

public partial class TrainerAvailability
{
    public int Id { get; set; }

    public int? TrainerId { get; set; }

    public int DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual Trainer? Trainer { get; set; }
}
