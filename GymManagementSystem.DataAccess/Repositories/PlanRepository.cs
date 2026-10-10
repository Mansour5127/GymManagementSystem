using GymManagementSystem.DataAccess.Data.Contexts;
using GymManagementSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DataAccess.Repositories
{

    public class PlanRepository(GymDbContext dbContext) : Repository<Plan>(dbContext), IPlanRepository
    {

        public void Add(Plan plan) 
            => dbContext.Add(plan);
        
        public void Delete(Plan plan) 
            => dbContext.Remove(plan);

        public async Task<IEnumerable<Plan>> GetAllAsync() 
            => await dbContext.Plans.ToListAsync();

        public Task<Plan?> GetById(int id) 
            => dbContext.Plans
            .FirstOrDefaultAsync(p => p.Id == id);


        public void Update(Plan plan)
            => dbContext.Update(plan);
        
        public async Task<int> SaveChangesAsync() 
            => await dbContext.SaveChangesAsync();

    }
}
