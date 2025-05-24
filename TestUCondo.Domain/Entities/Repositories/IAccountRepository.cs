namespace TestUCondo.Domain.Entities.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task AddAsync(Account Account, CancellationToken cancellationToken = default);
        Task DeleteAsync(Account Account, CancellationToken cancellationToken = default);        
        Task<Account> GetAsync(long Id, CancellationToken cancellationToken = default);
        Task<(IEnumerable<Account> Accounts, int TotalCount)> GetPaginatedAsync(
            int page, int pageSize, string? search, CancellationToken cancellationToken);
        Task UpdateAsync(Account entity, CancellationToken cancellationToken = default);
    }
}
