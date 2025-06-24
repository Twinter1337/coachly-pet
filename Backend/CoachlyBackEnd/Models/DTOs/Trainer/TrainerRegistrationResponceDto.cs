using CoachlyBackEnd.Models.DTOs.UserDtos;

namespace CoachlyBackEnd.Models.DTOs.TrainerDtos;

public class TrainerRegistrationResponceDto
{
    public UserDto User { get; set; }
    public TrainerDto? Trainer { get; set; }
}