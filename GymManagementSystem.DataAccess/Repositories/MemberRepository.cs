using GymManagementSystem.DataAccess.Data.Contexts;
using GymManagementSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymManagementSystem.DataAccess.Repositories
{
    internal class MemberRepository(GymDbContext dbContext) : Repository<Member>(dbContext), IMemberRepository
    {
        public async Task<bool> ExistsDataAsync(Expression<Func<Member, bool>> predicate, CancellationToken ct = default)
            => await dbContext.Set<Member>().AnyAsync(predicate, ct);
    }
}