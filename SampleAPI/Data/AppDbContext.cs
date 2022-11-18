using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;
using SampleAPI.Models.Config;
using Microsoft.Extensions.Configuration;
using static SampleAPI.Program;
using SampleAPI.Data.Config;

namespace SampleAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    var appSettings = Program.Configuration.GetSection("AppSettings").Get<AppSettings>();
            //    optionsBuilder.UseSqlServer(appSettings.ConnectionString);
            //}
            //base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=.;initial catalog=DbSampleAPI;integrated security=True;multipleactiveresultsets=True;application name=DbSampleAPI");
            }
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());

            base.OnModelCreating(modelBuilder);
        }

    }
}
