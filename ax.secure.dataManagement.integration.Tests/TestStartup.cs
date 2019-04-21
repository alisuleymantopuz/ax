using ax.storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ax.secure.dataManagement.integration.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {

        }

        public override void RegisterDatabaseContext(IServiceCollection services)
        {
            services.AddDbContext<ZipArchiveDBContext>(options => options.UseInMemoryDatabase("ZipArchiveDBContext"));
        }

        public override void ConfigureAdditionalMiddleware(IApplicationBuilder app)
        {
            base.ConfigureAdditionalMiddleware(app);
        }
    }
}
