using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models;

public partial class SessionParticipant
{
    public int Id { get; set; }

    public int? SessionId { get; set; }

    public int? UserIs { get; set; }

    public DateTime JoinedAt { get; set; }

    public virtual Session? Session { get; set; }

    public virtual User? UserIsNavigation { get; set; }
}
