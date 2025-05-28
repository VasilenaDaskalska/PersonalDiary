using PersonalDiary.Business.Models.Diaries;

namespace PersonalDiary.Services.Contracts
{
    public interface IDiaryService
    {
        Task<IEnumerable<DiaryBiz>> GetAllAsync();

        Task<DiaryBiz> GetByIdAsync(long id);

        Task<DiaryBiz> UpdateAsync(DiaryBiz model);

        Task<bool> DeleteAsync(long id);

        Task<DiaryBiz> CreateAsync(DiaryBiz model);
    }
}
