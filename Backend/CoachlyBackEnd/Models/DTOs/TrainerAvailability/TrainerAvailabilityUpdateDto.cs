namespace CoachlyBackEnd.Models.DTOs.TrainerAvailability;

public class TrainerAvailabilityUpdateDto
{
    public int? TrainerId { get; set; }
    public int? DayOfWeek { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
}