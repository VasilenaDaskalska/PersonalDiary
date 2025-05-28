using Microsoft.EntityFrameworkCore;
using PersonalDiary.Business.Models.Users;
using PersonalDiary.Entities.Context;
using PersonalDiary.HttpRepositories.Contracts;
using PersonalDiary.HttpRepositories.Helpers;
using PersonalDiary.Mappers;

namespace PersonalDiary.HttpRepositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly PersonalDiaryDbContext context;

        public AuthRepository(PersonalDiaryDbContext context)
        {
            this.context = context;
        }

        public async Task<UserBiz> ValidateUserCredentialsAsync(string username, string password)
        {
            string hashedPassword = string.Empty;

            if (username == "Admin")
            {
                hashedPassword = password;
            }
            else
            {
                // Hash the password for comparison
                hashedPassword = PasswordHelper.HashPasswordSHA512(password);
            }

            // Find user by username or email
            var user = await this.context.Users
                .FirstOrDefaultAsync(u =>
                    (u.UserName == username || u.Email == username) &&
                    u.Password == hashedPassword &&
                    !u.IsDeleted);

            if (user == null)
            {
                return null;
            }

            // Map User entity to UserBiz
            var userBiz = UserMapper.MapUserBizFromUserEntity(user);

            return userBiz;
        }
    }
}
