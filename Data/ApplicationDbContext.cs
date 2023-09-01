using Event_Management.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Data{
    public class ApplicationDbContext : DbContext{
        public DbSet<User> Users {get; set; }
        public DbSet<Event> Events {get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
    }
}