using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.SessionParticipants;

public class SessionParticipantDto
{
    public int Id { get; set; }

    public int? SessionId { get; set; }

    public int? UserIs { get; set; }

    public DateTime JoinedAt { get; set; }
    
    public SessionParticipantsStatus Status { get; set; }
}