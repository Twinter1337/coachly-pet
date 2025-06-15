using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.Specialization;

public class SpecializationCreateDto
{
    [Required]
    public string Name { get; set; }
}