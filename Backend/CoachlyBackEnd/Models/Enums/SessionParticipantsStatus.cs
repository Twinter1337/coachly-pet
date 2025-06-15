using NpgsqlTypes;

namespace CoachlyBackEnd.Models.Enums;

public enum SessionParticipantsStatus
{
    [PgName("accepted")]
    Accepted,
    [PgName("pending")]
    Pending,
    [PgName("rejected")]
    Rejected
}