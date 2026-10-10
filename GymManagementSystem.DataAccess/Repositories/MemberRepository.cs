using GymManagementSystem.DataAccess.Data.Contexts;
using GymManagementSystem.DataAccess.Models;

namespace GymManagementSystem.DataAccess.Repositories
{
    internal class MemberRepository(GymDbContext dbContext) : Repository<Member>(dbContext), IMemberRepository
    {

    }
}