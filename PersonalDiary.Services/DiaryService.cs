using PersonalDiary.Business.Models.Diaries;
using PersonalDiary.Entities;
using PersonalDiary.HttpRepositories.Contracts;
using PersonalDiary.Mappers;
using PersonalDiary.Services.Contracts;

namespace PersonalDiary.Services
{
    public class DiaryService : IDiaryService
    {
        private readonly IRepository<Diary> Repository;

        public DiaryService(
            IRepository<Diary> measureRepo)
        {
            this.Repository = measureRepo;
        }

        public async Task<DiaryBiz?> GetByIdAsync(long id)
        {
            try
            {
                var measure = await this.Repository.GetByIdAsync(id);
                return (measure == null || measure.IsDeleted)
                    ? null
                    : DiaryMapper.MapDiaryBizFromDiaryEntity(measure);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get diary {ex.Message}");
            }

        }

        public async Task<IEnumerable<DiaryBiz>> GetAllAsync()
        {
            try
            {
                var measures = await this.Repository.GetAllAsync();
                return measures
                    .Where(p => !p.IsDeleted)
                    .Select(DiaryMapper.MapDiaryBizFromDiaryEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get diaries {ex.Message}");
            }
        }

        public async Task<DiaryBiz> CreateAsync(DiaryBiz model)
        {
            try
            {
                var entity = DiaryMapper.MapDiaryEntityFromDiaryBiz(model);
                var created = await this.Repository.AddAsync(entity);

                //// await this.LogChange(created, model.UserCreatorID);
                return DiaryMapper.MapDiaryBizFromDiaryEntity(created);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create diary {ex.Message}");
            }

        }

        public async Task<DiaryBiz> UpdateAsync(DiaryBiz model)
        {
            try
            {
                var existing = await this.Repository.GetByIdAsync(model.ID);

                if (existing == null || existing.IsDeleted)
                {
                    throw new Exception("Diary not found.");
                }

                DiaryMapper.MapDiaryEntityFromDiaryBiz(ref existing, ref model);
                await this.Repository.UpdateAsync(existing);

                return await this.GetByIdAsync(model.ID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update diary {ex.Message}");
            }

        }

        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                var entity = await this.Repository.GetByIdAsync(id);
                long userId = 19;

                if (entity == null || entity.IsDeleted)
                {
                    throw new Exception("Diary not found.");
                }

                entity.IsDeleted = true;
                entity.LastModifiedDate = DateTime.UtcNow;
                entity.LastUserModifiedID = userId;

                await this.Repository.UpdateAsync(entity);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete diary {ex.Message}");
            }
        }
    }
}
