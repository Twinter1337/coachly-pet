using CoachlyBackEnd.Models.DTOs.TrainerDtos;

namespace CoachlyBackEnd.Models.DTOs.UserDtos;

public class VerifyResponse
{
    public UserDto User { get; set; }
    public TrainerDto? Trainer { get; set; }
    public string Token { get; set; }
}