using Microsoft.EntityFrameworkCore;
using TestUCondo.Domain.Entities;
using TestUCondo.Domain.Entities.Repositories;
using TestUCondo.Infra.Data.Context;
using TestUCondo.Infra.Data.Repository.Base;

namespace TestUCondo.Infra.Data.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly DefaultDbContext _context;

        public AccountRepository(DefaultDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Account?> GetAsync(long Id, CancellationToken cancellationToken = default)
        {
            return await _context.Accounts.Include(a => a.Filhos)
                        .FirstOrDefaultAsync(a => a.Id == Id, cancellationToken);
        }

        public async Task<List<Account>> GetAllByPrefixAsync(string prefix, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .Where(a => a.Codigo.StartsWith(prefix + "."))
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Account Account, CancellationToken cancellationToken = default)
        {
            await _context.Accounts.AddAsync(Account, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Account entity, CancellationToken cancellationToken = default)
        {
            _context.Set<Account>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Account Account, CancellationToken cancellationToken = default)
        {
            _context.Set<Account>().Remove(Account);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<(IEnumerable<Account> Accounts, int TotalCount)> GetPaginatedAsync(
            int page, int pageSize, string? search, CancellationToken cancellationToken)
        {
            var query = _context.Set<Account>().Include(a => a.Filhos).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u => u.Descricao.Contains(search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var Accounts = await query
                .OrderBy(u => u.Descricao)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (Accounts, totalCount);
        }
    }
}
