namespace CoachlyBackEnd.Models.DTOs.TrainerAvailability;

public class TrainerAvailabilityUpdateDto
{
    public int? TrainerId { get; set; }
    public int? DayOfWeek { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}