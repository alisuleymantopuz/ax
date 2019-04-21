using ax.encryptionProvider;
using ax.secure.dataManagement.Authentication;
using ax.secure.dataManagement.Formatters;
using ax.secure.dataManagement.Utils;
using ax.storage;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using ax.secure.dataManagement.Middleware;
using Microsoft.Extensions.Logging;
using System;

namespace ax.secure.dataManagement
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
            services.AddCors();

            services.AddAutoMapper();

            services.AddMvc(options =>
            {
                options.InputFormatters.Insert(0, new RawJsonBodyInputFormatter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.RegisterEncryptionProviderModule(Configuration);

            services.RegisterStorageModule(Configuration);

            RegisterDatabaseContext(services);

            RegiterSecurity(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ax Secure - v1", Version = "v1" });
            });

        }

        public virtual void RegiterSecurity(IServiceCollection services)
        {
            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<IUserProvider, UserProvider>();

            services.AddScoped<IUserService, UserService>();
        }

        public virtual void RegisterDatabaseContext(IServiceCollection services)
        {
            services.RegisterDataContext(Configuration, Configuration["InMemoryTable"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/axSecureDataManagement-{Date}.txt");

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ZipArchiveDBContext>();

                context.Database.EnsureCreated();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ax Secure - v1");
            });

            app.UseAuthentication();
            app.UseMiddleware<ExceptionHandler>();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            ConfigureAdditionalMiddleware(app);

            app.UseMvc();
        }

        public virtual void ConfigureAdditionalMiddleware(IApplicationBuilder app)
        {

        }
    }
}
