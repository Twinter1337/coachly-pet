using NpgsqlTypes;

namespace CoachlyBackEnd.Models.Enums;

public enum DocumentType
{
    [PgName("certificate")]
    Certificate,
    [PgName("diploma")]
    Diploma
}