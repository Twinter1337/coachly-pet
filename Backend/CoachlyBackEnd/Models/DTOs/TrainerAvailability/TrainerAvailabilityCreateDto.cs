using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.TrainerAvailability;

public class TrainerAvailabilityCreateDto
{
    [Required]
    public int TrainerId { get; set; }

    [Required]
    public int DayOfWeek { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }
}