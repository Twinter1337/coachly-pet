using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.UserDtos;

public class VerifyOtpDto
{
    [EmailAddress] public string Email { get; set; } = null!;
    public string OtpCode { get; set; } = null!;
}