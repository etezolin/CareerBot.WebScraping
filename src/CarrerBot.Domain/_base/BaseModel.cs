namespace CarrerBot.Domain._base;

public abstract class BaseModel
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public int IsDeleted { get; set; } = 0;
}
