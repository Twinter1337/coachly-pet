using NpgsqlTypes;

namespace CoachlyBackEnd.Models.Enums;

public enum CurrencyType
{
    [PgName("USD")]
    USD,
    [PgName("EUR")]
    EUR,
    [PgName("UAH")]
    UAH
}