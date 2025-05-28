using Microsoft.EntityFrameworkCore;

namespace PersonalDiary.Entities.Context
{
    public class PersonalDiaryDbContext : DbContext
    {
        public PersonalDiaryDbContext(DbContextOptions<PersonalDiaryDbContext> options)
               : base(options)
        {
        }

        public PersonalDiaryDbContext(string connectionString)
            : base(new DbContextOptionsBuilder<PersonalDiaryDbContext>().UseSqlServer(connectionString).Options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Diary> Diaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
