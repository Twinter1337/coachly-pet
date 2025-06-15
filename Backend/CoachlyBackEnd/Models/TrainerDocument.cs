using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models;

public partial class TrainerDocument
{
    public int Id { get; set; }

    public int? TrainerId { get; set; }

    public string FileName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateTime IssuedDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public DateTime UploadedAt { get; set; }
    
    public DocumentType DocumentType { get; set; }

    public virtual Trainer? Trainer { get; set; }
}
