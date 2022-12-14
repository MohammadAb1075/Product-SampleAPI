using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleAPI.Data.Config;
using SampleAPI.Entities;
using SampleAPI.Models.Config;
using System;

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

            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("data source=.;initial catalog=DbSampleAPI;integrated security=True;multipleactiveresultsets=True;application name=DbSampleAPI");
            //}
            //base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("AppSettings"));
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
