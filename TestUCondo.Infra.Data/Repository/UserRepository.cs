using Microsoft.EntityFrameworkCore;
using TestUCondo.Domain.Entities;
using TestUCondo.Domain.Entities.Repositories;
using TestUCondo.Infra.Data.Context;
using TestUCondo.Infra.Data.Repository.Base;

namespace TestUCondo.Infra.Data.Repository
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DefaultDbContext context) : base(context)
        {
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

        public async Task UpdateAsync(User entity)
        {
            _context.Set<User>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<User>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
