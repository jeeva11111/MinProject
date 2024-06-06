using Microsoft.EntityFrameworkCore;
using MinProject.Models;
using MinProject.Models.Customer;
using MinProject.Models.Student;
using MinProject.Views.Message;

namespace MinProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Notifaction> Notifications { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<MessageModel> Messages { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }

}
