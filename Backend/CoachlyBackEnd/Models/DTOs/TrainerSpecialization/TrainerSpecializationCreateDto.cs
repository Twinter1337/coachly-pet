using System.ComponentModel.DataAnnotations;

namespace CoachlyBackEnd.Models.DTOs.TrainerSpecialization;

public class TrainerSpecializationCreateDto
{
    [Required]
    public int? TrainerId { get; set; }

    [Required]
    public int? SpecalizationId { get; set; }
}