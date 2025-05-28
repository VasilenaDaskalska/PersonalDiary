using PersonalDiary.Business.Models.Users;

namespace PersonalDiary.Services.Contracts
{
    public interface IUserService
    {
        Task<UserBiz> RegisterUserAsync(UserBiz model);

        Task<bool> ChangePasswordAsync(long userId, string newPassword);

        Task<bool> DeleteUserAsync(long userId);

        Task<UserBiz> EditUserAsync(UserBiz model);

        Task<UserBiz?> GetByIdAsync(long id);

        Task<IEnumerable<UserBiz>> GetAllAsync();
    }
}
