using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.UserDtos;

public class LoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}