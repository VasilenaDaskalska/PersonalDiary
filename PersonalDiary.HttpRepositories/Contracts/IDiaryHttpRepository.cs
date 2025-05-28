using PersonalDiary.Business.Models.Diaries;

namespace PersonalDiary.HttpRepositories.Contracts
{
    public interface IDiaryHttpRepository
    {
        Task<DiaryBiz?> GetByIdAsync(long id);

        Task<IEnumerable<DiaryBiz>> GetAllAsync();

        Task<DiaryBiz> CreateAsync(DiaryBiz model);

        Task UpdateAsync(DiaryBiz user);

        Task<bool> DeleteAsync(long id);
    }
}
