namespace CoachlyBackEnd.Models;

public partial class Location
{
    public int Id { get; set; }

    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string BuildingNumber { get; set; } = null!;

    public string GymName { get; set; } = null!;

    public virtual ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();
}
