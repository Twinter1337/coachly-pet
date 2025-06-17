using System.ComponentModel.DataAnnotations;
using CoachlyBackEnd.Models.DTOs.Location;
using CoachlyBackEnd.Models.DTOs.UserDtos;

namespace CoachlyBackEnd.Models.DTOs.TrainerDtos;

public class TrainerRegisterDto
{
    [Required]
    public UserCreateDto User { get; set; }
    
    [Required]
    public string Bio { get; set; }
    
    public LocationCreateDto Location { get; set; }
}