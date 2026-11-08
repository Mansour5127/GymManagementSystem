using GymManagementSystem.DataAccess.Enums;

namespace GymManagementSystem.DataAccess.Models
{
    public class HealthRecord : BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string? Note { get; set; }
        public BloodType BloodType { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; } = default!;
    }
}
