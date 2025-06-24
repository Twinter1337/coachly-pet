namespace CoachlyBackEnd.Models.DTOs.TrainerAvailability;

public class TrainerAvailabilityDto
{
    public int Id { get; set; }
    
    public int TrainerId { get; set; }

    public int DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}