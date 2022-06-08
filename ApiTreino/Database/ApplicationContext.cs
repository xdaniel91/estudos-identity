using ApiTreino.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ApiTreino
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> persons { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}
