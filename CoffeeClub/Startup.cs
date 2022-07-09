using AutoMapper;
using Contracts;
using Entities;
using FluentValidation.AspNetCore;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Repository;
using System;
using System.IO;
using System.Reflection;

namespace CoffeeClub
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

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services
                .AddMvcCore()
                .AddApiExplorer()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                    s.DisableDataAnnotationsValidation = true;
                    s.AutomaticValidationEnabled = true;
                    s.ImplicitlyValidateChildProperties = true;
                });
            //services.AddDbContext<CoffeeClubContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));
            services.AddDbContext<CoffeeClubContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase("InMemoryDb"));
            services.AddScoped<IDbInitializer, DbInitializer>();
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoffeeClub", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<ILoggerManager, LoggerManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoffeeClub v1"));
            }
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                dbInitializer.Initialize();
                dbInitializer.SeedData();
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
