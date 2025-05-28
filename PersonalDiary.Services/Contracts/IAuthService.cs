using PersonalDiary.Business.Models.Users;

namespace PersonalDiary.Services.Contracts
{
    public interface IAuthService
    {
        Task<SignInResponseModel> SignInAsync(SignInRequestModel request);
    }
}
