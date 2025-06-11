public class Company
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Industry { get; set; }
    public string Size { get; set; }
    public Location Headquarters { get; set; }
    public string Description { get; set; }
    public string Website { get; set; }
    public List<string> TechStack { get; set; }
    public decimal? GlassdoorRating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
