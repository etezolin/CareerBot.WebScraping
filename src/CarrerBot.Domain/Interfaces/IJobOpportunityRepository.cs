using CarrerBot.Domain._base.Interface;

public interface IJobOpportunityRepository : IBaseRepository<JobOpportunity>
{
    Task<IEnumerable<JobOpportunity>> GetAllActiveAsync();
    Task<IEnumerable<JobOpportunity>> GetBySkillsAsync(List<string> skills);
#nullable enable
    Task<JobOpportunity?> GetBySourceUrlAsync(string sourceUrl);
#nullable restore
    Task<Guid> CreateAsync(JobOpportunity jobOpportunity);
    Task UpdateAsync(JobOpportunity jobOpportunity);
    Task DeactivateAsync(Guid id);
    Task<IEnumerable<JobOpportunity>> GetRecentAsync(int days = 7);
    Task<int> GetTotalCountAsync();
    Task BulkInsertAsync(IEnumerable<JobOpportunity> jobs);
}
