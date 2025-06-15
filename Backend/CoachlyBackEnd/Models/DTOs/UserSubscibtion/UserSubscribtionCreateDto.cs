using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.UserSubscibtion;

public class UserSubscribtionCreateDto
{
    [Required]
    public int SubscribtionId { get; set; }

    [Required]
    public int UserId { get; set; }
}