using GymManagementSystem.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Presentation.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository _planRepo;

        public PlanController(IPlanRepository planRepo)
        {
            _planRepo = planRepo;
        }

        public async Task<IActionResult> Index()
        {
            var Plans = await _planRepo.GetAllAsync();

            return View(Plans);
        }

        public async Task<IActionResult> Details(int id)
        {
            var plans = await _planRepo.GetByIdAsync(id);

            if (plans == null)
            {
                return RedirectToAction(nameof(Index)); // return 302, Location: /Plan/Index
            }

            if(plans.Id <= 0) {
                return NotFound();
            }

            return View(plans);
        }
    }
}