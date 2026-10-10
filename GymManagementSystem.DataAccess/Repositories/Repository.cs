using GymManagementSystem.DataAccess.Data.Contexts;
using GymManagementSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymManagementSystem.DataAccess.Repositories
{
    public class Repository<TEntity>(GymDbContext dbContext) : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly GymDbContext _dbContext = dbContext;
        private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
            => await _dbSet
            .AsNoTracking()
            .ToListAsync(ct);

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _dbSet
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        public async Task<TEntity?> GetByIdIncludingDeletedAsync(int id, CancellationToken ct = default)
            => await _dbSet
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        public async Task<bool> ExistsAsync(int id, CancellationToken ct = default)
            => await _dbSet
            .AnyAsync(e => e.Id == id, ct);

        public async Task<IReadOnlyList<TEntity>> FindAsync
            (Expression<Func<TEntity, bool>> predicate, 
              CancellationToken ct = default)
                => await _dbSet
                .Where(predicate)
                .ToListAsync(ct);

        public async Task AddAsync(TEntity entity, CancellationToken ct = default)
            => await _dbSet
            .AddAsync(entity, ct);

        public Task SoftDeleteAsync(TEntity entity, CancellationToken ct = default)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
            
            return Task.CompletedTask;
        }

        public void Update(TEntity entity)
            => _dbSet
            .Update(entity);

        public Task<int> SaveChangesAsync()
            => _dbContext
            .SaveChangesAsync();
    }
}
