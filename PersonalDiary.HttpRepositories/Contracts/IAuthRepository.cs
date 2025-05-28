using PersonalDiary.Business.Models.Users;

namespace PersonalDiary.HttpRepositories.Contracts
{
    public interface IAuthRepository
    {
        Task<UserBiz> ValidateUserCredentialsAsync(string username, string password);
    }
}
