using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PersonalDiary.Entities.Contracts;

namespace PersonalDiary.Entities.Base
{
    public abstract class DiaryBaseEntity : BaseEntity, IDeletable, IAuditInfo
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required!")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "The title must be between 5 and 50 characters long!")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "The description must be maximum 500 characters long!")]
        public string Description { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public long UserCreatorID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        public long LastUserModifiedID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastModifiedDate { get; set; }
    }
}
