using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.SessionParticipants;

public class SessionParticipantUpdateDto
{
    public int? SessionId { get; set; }

    public int? UserIs { get; set; }

    public DateTime? JoinedAt { get; set; }
}