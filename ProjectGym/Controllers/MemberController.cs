using GymManagementSystem.BusinessLogic.Servives;
using GymManagementSystem.BusinessLogic.ViewModels.Members;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Presentation.Controllers
{
    public class MemberController(IMemberService memberService) : Controller
    {
        // get all members
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var members = await memberService.GetAllAsync(cancellationToken);

            return View(members);
        }


        // create member

        [HttpGet]
        public IActionResult Create() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateMemberViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await memberService.CreateMemberAsync(model, ct);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors)
                {
                    foreach (var message in error.Value)
                    {
                        ModelState.AddModelError(
                            error.Key,
                            message);
                    }
                }

                return View(model);
            }

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Member created successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create member.";
            }

            return RedirectToAction(nameof(Index));
        }




        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(
        //    CreateMemberViewModel model,
        //    CancellationToken ct)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var result = await memberService.CreateMemberAsync(model, ct);

        //    if (!result.IsSuccess)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            foreach (var message in error.Value)
        //            {
        //                ModelState.AddModelError(error.Key, message);
        //            }
        //        }

        //        return View(model);
        //    }

        //    TempData["SuccessMessage"] = "Member created successfully.";

        //    return RedirectToAction(nameof(Index));
        //}


        // 1. jquery.validate.min. js => client side validation (unobtrusive validation)
        // 2. validate.unobtrusive.min. js => client side validation (unobtrusive validation)
    }
}
