using System.Linq.Expressions;
using PersonalDiary.Business.Models.Users;
using PersonalDiary.Entities;
using PersonalDiary.Entities.ENUMS;

namespace PersonalDiary.Mappers
{
    public class UserMapper
    {
        public static Expression<Func<User, UserBiz>> SelectUserBizFromUserEntity => (userEntity) => new UserBiz()
        {
            ID = userEntity.ID,
            Name = userEntity.Name,
            UserCreatorID = userEntity.UserCreatorID,
            LastUserModifiedID = userEntity.LastUserModifiedID,
            CreationDate = userEntity.CreationDate,
            LastModifiedDate = userEntity.LastModifiedDate,
            IsDeleted = userEntity.IsDeleted,
            Password = userEntity.Password,
            Email = userEntity.Email,
            UserName = userEntity.UserName,
            PhoneNumber = userEntity.PhoneNumber,
            LastPasswordModifiedDate = userEntity.LastPasswordModifiedDate,
            UserPermissions = userEntity.UserPermissions,
        };

        public static void MapUserEntityFromUserBiz(ref User userEntity, ref UserBiz userBiz)
        {
            userEntity.ID = userBiz.ID;
            userEntity.Name = userBiz.Name;
            userEntity.UserCreatorID = userBiz.UserCreatorID;
            userEntity.LastUserModifiedID = userBiz.LastUserModifiedID;
            userEntity.CreationDate = userBiz.CreationDate.ToUniversalTime();
            userEntity.LastModifiedDate = userBiz.LastModifiedDate.ToUniversalTime();
            userEntity.IsDeleted = userBiz.IsDeleted;
            userEntity.Password = userBiz.Password;
            userEntity.Email = userBiz.Email;
            userEntity.UserName = userBiz.UserName;
            userEntity.PhoneNumber = userBiz.PhoneNumber;
            userEntity.LastPasswordModifiedDate = userBiz.LastPasswordModifiedDate?.ToUniversalTime();
            userEntity.UserPermissions = PERMISSIONS.User;
        }

        public static User MapUserEntityFromUserBiz(UserBiz userBiz)
        {
            return new User()
            {
                ID = userBiz.ID,
                Name = userBiz.Name,
                UserCreatorID = userBiz.UserCreatorID,
                LastUserModifiedID = userBiz.LastUserModifiedID,
                CreationDate = userBiz.CreationDate.ToUniversalTime(),
                LastModifiedDate = userBiz.LastModifiedDate.ToUniversalTime(),
                IsDeleted = userBiz.IsDeleted,
                Password = userBiz.Password,
                Email = userBiz.Email,
                UserName = userBiz.UserName,
                PhoneNumber = userBiz.PhoneNumber,
                LastPasswordModifiedDate = userBiz.LastPasswordModifiedDate?.ToUniversalTime(),
                UserPermissions = PERMISSIONS.User,
            };
        }

        public static User MapUserEntityWithAdminFromUserBiz(UserBiz userBiz)
        {
            return new User()
            {
                ID = userBiz.ID,
                Name = userBiz.Name,
                UserCreatorID = userBiz.UserCreatorID,
                LastUserModifiedID = userBiz.LastUserModifiedID,
                CreationDate = userBiz.CreationDate.ToUniversalTime(),
                LastModifiedDate = userBiz.LastModifiedDate.ToUniversalTime(),
                IsDeleted = userBiz.IsDeleted,
                Password = userBiz.Password,
                Email = userBiz.Email,
                UserName = userBiz.UserName,
                PhoneNumber = userBiz.PhoneNumber,
                LastPasswordModifiedDate = userBiz.LastPasswordModifiedDate?.ToUniversalTime(),
                UserPermissions = PERMISSIONS.Admin,
            };
        }

        public static UserBiz MapUserBizFromUserEntity(User userEntity)
        {
            return new UserBiz()
            {
                ID = userEntity.ID,
                Name = userEntity.Name,
                UserCreatorID = userEntity.UserCreatorID,
                LastUserModifiedID = userEntity.LastUserModifiedID,
                CreationDate = userEntity.CreationDate.ToUniversalTime(),
                LastModifiedDate = userEntity.LastModifiedDate.ToUniversalTime(),
                IsDeleted = userEntity.IsDeleted,
                Password = userEntity.Password,
                Email = userEntity.Email,
                UserName = userEntity.UserName,
                LastPasswordModifiedDate = userEntity.LastPasswordModifiedDate?.ToUniversalTime(),
                PhoneNumber = userEntity.PhoneNumber,
                UserPermissions = userEntity.UserPermissions,
            };
        }
    }
}
