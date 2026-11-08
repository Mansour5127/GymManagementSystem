namespace GymManagementSystem.BusinessLogic.ViewModels.Members
{
    public class MemberIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = null!;
        public string Phone { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime JoinDate { get; set; }
        public string Gender { get; set; } = null!; // store in DB as string if donot store as string make it enum
    }
}
