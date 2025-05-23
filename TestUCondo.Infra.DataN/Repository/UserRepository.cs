using Microsoft.EntityFrameworkCore;
using TestUCondo.Domain.Entities;
using TestUCondo.Domain.Entities.Repositories;
using TestUCondo.Infra.Data.Context;
using TestUCondo.Infra.Data.Repository.Base;

namespace TestUCondo.Infra.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DefaultDbContext _context;

        public UserRepository(DefaultDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task AddAsync(User entity)
        {
            await _context.Set<User>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Usuarios.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }

        public async Task UpdateAsync(User entity)
        {
            _context.Set<User>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Set<User>().Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<(IEnumerable<User> Users, int TotalCount)> GetPaginatedAsync(
            int page, int pageSize, string? search, CancellationToken cancellationToken)
        {
            var query = _context.Set<User>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u => u.Name.Contains(search) || u.Email.Contains(search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var users = await query
                .OrderBy(u => u.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (users, totalCount);
        }
    }
}
