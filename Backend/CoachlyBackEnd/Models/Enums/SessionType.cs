using NpgsqlTypes;

namespace CoachlyBackEnd.Models.Enums;

public enum SessionType
{
    [PgName("individual")]
    Individual,
    [PgName("group")]
    Group
}