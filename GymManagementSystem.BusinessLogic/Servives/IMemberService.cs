using GymManagementSystem.BusinessLogic.ViewModels.Members;

namespace GymManagementSystem.BusinessLogic.Servives
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberIndexViewModel>> GetAllAsync(CancellationToken ct = default);
        Task<Result> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default);
        //Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default);
    }
}