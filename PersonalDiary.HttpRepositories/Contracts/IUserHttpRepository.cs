using PersonalDiary.Business.Models.Users;

namespace PersonalDiary.HttpRepositories.Contracts
{
    public interface IUserHttpRepository
    {
        Task<UserBiz> RegisterUserAsync(UserBiz user);

        Task<bool> ChangePasswordAsync(long userId, string newPassword);

        Task<IEnumerable<UserBiz>> GetAllAsync();

        Task<UserBiz> GetByIdAsync(long id);

        Task UpdateAsync(UserBiz user);

        Task<bool> DeleteAsync(long id);

        Task<SignInResponseModel> SignInAsync(string username, string password);
    }
}
