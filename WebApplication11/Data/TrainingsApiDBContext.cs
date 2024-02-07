using Microsoft.EntityFrameworkCore;
using WebApplication11.model;
using Training.model;

namespace Training.Data
{
    public class TrainingsApiDBContext: DbContext
    {
        public TrainingsApiDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Trainings> Trainings { get; set; }
        public DbSet<Resource>Resources1 { get; set; }
        public DbSet<UserRole>UserRoles { get; set; }
        public DbSet<TrainingAssociate> TrainingAssociates {  get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }



    }
}
