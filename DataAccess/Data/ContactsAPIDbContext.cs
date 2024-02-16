using Microsoft.EntityFrameworkCore;
using DataAccess.model;

namespace DataAccess.Data
{
    public class ContactsAPIDbContext:DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }

       
    }
   
}
