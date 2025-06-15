using System.ComponentModel.DataAnnotations;
using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.UserDtos;

public class UserCreateDto
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required, MinLength(6)]
    public string PasswordHash { get; set; } = null!;
    [Required, MinLength(9)]
    public string Phone { get; set; } = null!;
    [Required]
    public UserRole Role { get; set; }
}