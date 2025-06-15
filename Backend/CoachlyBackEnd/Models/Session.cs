using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models;

public partial class Session
{
    public int Id { get; set; }

    public int? TrainerId { get; set; }

    public int DurationMinutes { get; set; }

    public int MaxParticipants { get; set; }

    public decimal Price { get; set; }

    public DateTime ScheduledAt { get; set; }

    public int? PaymentId { get; set; }

    public SessionStatus Status { get; set; }
    
    public SessionType Type { get; set; }
    
    public virtual Payment? Payment { get; set; }

    public virtual ICollection<SessionParticipant> SessionParticipants { get; set; } = new List<SessionParticipant>();

    public virtual Trainer? Trainer { get; set; }
}
