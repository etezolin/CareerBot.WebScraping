public class JobOpportunity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public string CompanyName { get; set; }
    public Guid CompanyId { get; set; }
    public Location Location { get; set; }
#nullable enable
    public Salary? Salary { get; set; }
#nullable restore
    public List<string> RequiredSkills { get; set; }
    public List<string> PreferredSkills { get; set; }
    public JobLevel Level { get; set; }
    public ContractType ContractType { get; set; }
    public string SourceUrl { get; set; }
    public string SourcePlatform { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime ScrapedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public int MatchScore { get; set; }
    public Company Company { get; set; }
}
