using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PersonalDiary.Entities.Contracts;
using PersonalDiary.Entities.ENUMS;

namespace PersonalDiary.Entities.Base
{
    public abstract class UserBaseEntity : BaseEntity, IDeletable, IAuditInfo
    {
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required!")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "The name must be between 5 and 50 characters long!")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required!")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "The username must be between 5 and 30 characters long!")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(maximumLength: 50, ErrorMessage = "The email must be maximum 30 characters long!")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(maximumLength: 20, ErrorMessage = "The phone number must be maximum 20 characters long!")]
        public string PhoneNumber { get; set; }

        [DefaultValue(PERMISSIONS.None)]
        public PERMISSIONS UserPermissions { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual long UserCreatorID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        public virtual long LastUserModifiedID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastModifiedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastPasswordModifiedDate { get; set; }
    }
}
