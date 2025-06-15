using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.SessionParticipants;

public class SessionParticipantCreateDto
{
    [Required]
    public int SessionId { get; set; }

    [Required]
    public int UserIs { get; set; }
}