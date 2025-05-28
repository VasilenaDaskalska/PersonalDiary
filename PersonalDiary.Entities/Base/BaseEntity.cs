using System.ComponentModel.DataAnnotations;

namespace PersonalDiary.Entities.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public long ID { get; set; }
    }
}
