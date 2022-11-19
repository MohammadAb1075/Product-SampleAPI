using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleAPI.Data;
using SampleAPI.Services;
using System.Text.Json.Serialization;

namespace SampleAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            //services.AddSwaggerDocument();

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Product Sample API";
                    document.Info.Description = "Web API";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Mohammad Abdollahzadeh",
                        Email = "mabdollah1375@gmail.com"
                    };
                };
            });

            services.AddDbContext<AppDbContext>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<AppDbContext, AppDbContext>();

            //var builder = new ConfigurationBuilder()
            //                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            ////Configuration = builder.Build();
            //Configuration.GetConnectionString("ConnectionString");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


                app.UseOpenApi();
                app.UseSwaggerUi3();

                //app.UseSwagger();
                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleAPI V1");
                //});

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
