using System.ComponentModel.DataAnnotations;
using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.Session;

public class SessionCreateDto
{
    [Required]
    public int TrainerId { get; set; }

    [Required]
    public int DurationMinutes { get; set; }

    [Required]
    public int MaxParticipants { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public DateTime ScheduledAt { get; set; }

    [Required]
    public int PaymentId { get; set; }
    
    public SessionStatus Status { get; set; }
    
    public SessionType Type { get; set; }
}