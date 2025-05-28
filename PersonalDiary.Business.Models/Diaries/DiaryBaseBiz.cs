using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonalDiary.Business.Models.Diaries
{
    public class DiaryBaseBiz
    {
        public long ID { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "The title must be between 5 and 50 characters long!")]
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
