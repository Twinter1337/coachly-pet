using CoachlyBackEnd.Models.Enums;

namespace CoachlyBackEnd.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    
    public UserRole Role { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<SessionParticipant> SessionParticipants { get; set; } = new List<SessionParticipant>();

    public virtual Trainer? Trainer { get; set; }

    public virtual ICollection<UserSubscribtion> UserSubscribtions { get; set; } = new List<UserSubscribtion>();
}
