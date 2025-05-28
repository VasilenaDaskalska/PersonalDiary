using System.ComponentModel;

namespace PersonalDiary.Entities.Contracts
{
    public interface IDeletable
    {
        [DefaultValue(false)]
        bool IsDeleted { get; set; }
    }
}
