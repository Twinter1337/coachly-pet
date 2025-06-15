using NpgsqlTypes;

namespace CoachlyBackEnd.Models.Enums;

public enum PaymentStatus
{
    [PgName("pending")]
    Pending,
    [PgName("succeeded")]
    Succeeded,
    [PgName("failed")]
    Failed,
    [PgName("canceled")]
    Canceled,
    [PgName("refunded")]
    Refunded,
    [PgName("expired")]
    Expired,
    [PgName("in_review")]
    InReview
}