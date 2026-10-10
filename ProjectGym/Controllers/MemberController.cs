using GymManagementSystem.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Presentation.Controllers
{
    public class MemberController(IMemberRepository memberRepository) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var members = await memberRepository.GetAllAsync(cancellationToken);
            return View(members);
        }
    }
}
