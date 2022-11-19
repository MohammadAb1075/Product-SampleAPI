using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SampleAPI.Data;
using SampleAPI.Services;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SampleAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            //services.AddSwaggerDocument();

            //services.AddSwaggerDocument(config =>
            //{
            //    config.PostProcess = document =>
            //    {
            //        document.Info.Version = "v1";
            //        document.Info.Title = "Product Sample API";
            //        document.Info.Description = "Web API";
            //        document.Info.Contact = new NSwag.OpenApiContact
            //        {
            //            Name = "Mohammad Abdollahzadeh",
            //            Email = "mabdollah1375@gmail.com"
            //        };
            //    };
            //});


            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Product Sample API",
                    Description = "Mohammad Abdollahzadeh",
                    License = new OpenApiLicense
                    {
                        Name = "Mohammad_Ab1075 _ mabdollah1375@gmail.com",            
                    }
                });
             var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[]{}},
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "Bearer",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });




            services.AddDbContext<AppDbContext>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<AppDbContext, AppDbContext>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });


            //var secretKey = Configuration.GetValue<string>("TokenKey");
            //var tokenTimeOut = Configuration.GetValue<int>("TokenTimeOut");
            //var key = Encoding.UTF8.GetBytes(secretKey);

            //services.AddAuthentication(x => {
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x => {
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ClockSkew = TimeSpan.FromMinutes(tokenTimeOut),
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});


            //services.AddCors(options => {
            //    options.AddPolicy("SampleAPI",
            //    b =>
            //    {
            //        b.WithOrigins("*");
            //        b.WithHeaders("*");
            //        b.WithMethods("*");
            //    });
            //});


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

                //NSwag.AspNetCore
                //app.UseOpenApi();
                //app.UseSwaggerUi3();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleAPI V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
