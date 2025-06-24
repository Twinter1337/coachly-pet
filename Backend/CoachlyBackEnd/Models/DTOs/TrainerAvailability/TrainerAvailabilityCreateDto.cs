using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.TrainerAvailability;

public class TrainerAvailabilityCreateDto
{
    [Required]
    public int TrainerId { get; set; }

    [Required]
    public int DayOfWeek { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }
}