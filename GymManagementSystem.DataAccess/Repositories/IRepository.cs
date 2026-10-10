using GymManagementSystem.DataAccess.Models;
using System.Linq.Expressions;

namespace GymManagementSystem.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<TEntity?> GetByIdIncludingDeletedAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
        Task<bool> ExistsAsync(int id, CancellationToken ct = default);
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        void Update(TEntity entity);
        Task SoftDeleteAsync(TEntity entity, CancellationToken ct = default);
        Task<int> SaveChangesAsync();
    }
}