using System.ComponentModel.DataAnnotations;
using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models.DTOs.TrainerDocument;

public class TrainerDocumentCreateDto
{
    [Required]
    public int TrainerId { get; set; }

    [Required]
    public string FileName { get; set; } = null!;

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public DateTime IssuedDate { get; set; }

    [Required]
    public DateTime ExpirationDate { get; set; }

    [Required]
    public DateTime UploadedAt { get; set; }
    
    [Required]
    public DocumentType DocumentType { get; set; }
}