namespace CoachlyBackEnd.Models.DTOs.TrainerSpecialization;

public class TrainerSpecializationDto
{
    public int Id { get; set; }

    public int? TrainerId { get; set; }

    public int? SpecalizationId { get; set; }
}