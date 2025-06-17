using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.TrainerDtos;

public class TrainerCreateDto
{
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public string Bio { get; set; }
    
    public int? LocationId { get; set; }
}