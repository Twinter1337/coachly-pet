using NpgsqlTypes;

namespace CoachlyBackEnd.Models.Enums;

public enum SessionStatus
{
    [PgName("scheduled")]
    Scheduled,
    [PgName("completed")]
    Completed,
    [PgName("canceled")]
    Canceled
}