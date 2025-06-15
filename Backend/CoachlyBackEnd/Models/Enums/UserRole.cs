using NpgsqlTypes;

namespace CoachlyBackEnd.Models.Enums;

public enum UserRole
{
    [PgName("Client")]
    Client,
    [PgName("Trainer")]
    Trainer,
    [PgName("Admin")]
    Admin
}