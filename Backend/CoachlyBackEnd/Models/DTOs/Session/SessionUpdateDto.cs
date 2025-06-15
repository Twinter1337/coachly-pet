using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.Session;

public class SessionUpdateDto
{
    public int? TrainerId { get; set; }

    public int? DurationMinutes { get; set; }

    public int? MaxParticipants { get; set; }

    public decimal? Price { get; set; }

    public DateTime? ScheduledAt { get; set; }

    public int? PaymentId { get; set; }
    
    public SessionStatus? Status { get; set; }
    
    public SessionType? Type { get; set; }
}