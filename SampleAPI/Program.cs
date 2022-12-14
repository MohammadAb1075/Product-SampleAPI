using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI
{
    public class Program
    {
        //public static IConfiguration Configuration;
        public static void Main(string[] args)
        {
            //var builder = new ConfigurationBuilder()
            //                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //Configuration = builder.Build();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
    //public class AppSettings
    //{
    //    public string ConnectionString { get; set; }
    //}
}
