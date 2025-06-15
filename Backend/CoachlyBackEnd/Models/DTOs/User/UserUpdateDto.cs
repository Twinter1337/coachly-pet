using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.UserDtos;

public class UserUpdateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; } 
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public UserRole? Role { get; set; }
}