using System.Linq.Expressions;
using PersonalDiary.Business.Models.Diaries;
using PersonalDiary.Entities;

namespace PersonalDiary.Mappers
{
    public class DiaryMapper
    {
        public static Expression<Func<Diary, DiaryBiz>> SelectDiaryBizFromDiaryEntity => (diaryEntity) => new DiaryBiz()
        {
            ID = diaryEntity.ID,
            Title = diaryEntity.Title,
            Description = diaryEntity.Description,
            UserCreatorID = diaryEntity.UserCreatorID,
            LastUserModifiedID = diaryEntity.LastUserModifiedID,
            CreationDate = diaryEntity.CreationDate.ToUniversalTime(),
            LastModifiedDate = diaryEntity.LastModifiedDate.ToUniversalTime(),
            IsDeleted = diaryEntity.IsDeleted,
        };

        public static void MapDiaryEntityFromDiaryBiz(ref Diary DiaryEntity, ref DiaryBiz DiaryBiz)
        {
            DiaryEntity.ID = DiaryBiz.ID;
            DiaryEntity.Title = DiaryBiz.Title;
            DiaryEntity.Description = DiaryBiz.Description;
            DiaryEntity.UserCreatorID = DiaryBiz.UserCreatorID;
            DiaryEntity.LastUserModifiedID = DiaryBiz.LastUserModifiedID;
            DiaryEntity.CreationDate = DiaryBiz.CreationDate.ToUniversalTime();
            DiaryEntity.LastModifiedDate = DiaryBiz.LastModifiedDate.ToUniversalTime();
            DiaryEntity.IsDeleted = DiaryBiz.IsDeleted;
        }


        public static DiaryBiz MapDiaryBizFromDiaryEntity(Diary DiaryEntity)
        {
            return new DiaryBiz()
            {
                ID = DiaryEntity.ID,
                Title = DiaryEntity.Title,
                UserCreatorID = DiaryEntity.UserCreatorID,
                LastUserModifiedID = DiaryEntity.LastUserModifiedID,
                CreationDate = DiaryEntity.CreationDate.ToUniversalTime(),
                LastModifiedDate = DiaryEntity.LastModifiedDate.ToUniversalTime(),
                IsDeleted = DiaryEntity.IsDeleted,
                Description = DiaryEntity.Description,
            };
        }

        public static Diary MapDiaryEntityFromDiaryBiz(DiaryBiz DiaryBiz)
        {
            return new Diary()
            {
                ID = DiaryBiz.ID,
                Title = DiaryBiz.Title,
                UserCreatorID = DiaryBiz.UserCreatorID,
                LastUserModifiedID = DiaryBiz.LastUserModifiedID,
                CreationDate = DiaryBiz.CreationDate.ToUniversalTime(),
                LastModifiedDate = DiaryBiz.LastModifiedDate.ToUniversalTime(),
                IsDeleted = DiaryBiz.IsDeleted,
                Description = DiaryBiz.Description,
            };
        }
    }
}