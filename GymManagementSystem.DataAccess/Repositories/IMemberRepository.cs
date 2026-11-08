using GymManagementSystem.DataAccess.Models;
using System.Linq.Expressions;

namespace GymManagementSystem.DataAccess.Repositories
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task<bool> ExistsDataAsync(Expression<Func<Member, bool>> predicate, CancellationToken ct = default);
    }
}
