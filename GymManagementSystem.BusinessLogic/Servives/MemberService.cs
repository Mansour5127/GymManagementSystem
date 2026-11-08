using GymManagementSystem.BusinessLogic.ViewModels.Members;
using GymManagementSystem.DataAccess.Enums;
using GymManagementSystem.DataAccess.Models;
using GymManagementSystem.DataAccess.Models.ValueObjects;
using GymManagementSystem.DataAccess.Repositories;

namespace GymManagementSystem.BusinessLogic.Servives
{
    public class MemberService(IMemberRepository memberRepository) : IMemberService
    {
        // ======================== GetAllAsync ======================== 
        public async Task<IEnumerable<MemberIndexViewModel>> GetAllAsync(CancellationToken ct = default)
        {
            var members = await memberRepository.GetAllAsync(ct);

            return members.Select(m => new MemberIndexViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                PhotoUrl = m.Photo,
                JoinDate = m.JoinDate,
                Gender = m.Gender.ToString()
            });
        }


        // ======================== CreateMemberAsync ======================== 

        public async Task<Result> CreateMemberAsync(
            CreateMemberViewModel model,CancellationToken ct = default)
        {
            var errors = new Dictionary<string, string[]>();

            var email = model.Email.Trim().ToLowerInvariant();
            var phone = model.Phone.Trim();
            var name = model.Name.Trim();

            // Check Email
            if (await memberRepository.ExistsDataAsync(
                m => m.Email == email, ct))
            {
                errors.Add(
                    nameof(model.Email),
                    ["Email already exists."]);
            }

            // Check Phone
            if (await memberRepository.ExistsDataAsync(
                m => m.Phone == phone, ct))
            {
                errors.Add(
                    nameof(model.Phone),
                    ["Phone number already exists."]);
            }

            // Parse Gender
            if (!Enum.TryParse<Gender>(
                    model.Gender, true, out var gender)
                || !Enum.IsDefined(typeof(Gender), gender))
            {
                errors.Add(
                    nameof(model.Gender),
                    ["Invalid gender."]);
            }

            // Parse Blood Type
            if (!Enum.TryParse<BloodType>(
                    model.HealthRecordViewModel.BloodType,
                    true,
                    out var bloodType)
                || !Enum.IsDefined(typeof(BloodType), bloodType))
            {
                errors.Add(
                    "HealthRecordViewModel.BloodType",
                    ["Invalid blood type."]);
            }

            // Return validation errors
            if (errors.Count > 0)
                return Result.Failure(errors);

            var member = new Member
            {
                Name = name,
                Email = email,
                Phone = phone,
                Gender = gender,
                DateOfBirth = model.DateOfBirth,

                Address = new Address
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },

                HealthRecord = new HealthRecord
                {
                    Height = model.HealthRecordViewModel.Height,
                    Weight = model.HealthRecordViewModel.Weight,
                    BloodType = bloodType,
                    Note = model.HealthRecordViewModel.Note
                },

                JoinDate = DateTime.UtcNow
            };

            await memberRepository.AddAsync(member, ct);

            var result = await memberRepository.SaveChangesAsync(ct);

            if (result <= 0)
            {
                return Result.Failure(
                    new Dictionary<string, string[]>
                    {
                        ["General"] = ["Failed to create member."]
                    });
            }

            return Result.Success();
        }
        


    }
}