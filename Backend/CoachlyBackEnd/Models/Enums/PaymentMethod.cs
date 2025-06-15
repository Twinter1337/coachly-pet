using NpgsqlTypes;

namespace CoachlyBackEnd.Models.Enums;

public enum PaymentMethod
{
    [PgName("card")]
    Card,
    [PgName("cash")]
    Cash
}