using CarrerBot.Domain._base;

namespace CarrerBot.Domain.Configuration;

public class ConfigurationModel : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TimeStart { get; set; }
    public string TimeEnd { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastExecution { get; set; }
}
