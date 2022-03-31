using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mre.Visas.Multa.Application.Repositories;
using Mre.Visas.Multa.Application.Multa.Repositories;
using Mre.Visas.Multa.Application.Shared.Interfaces;
using Mre.Visas.Multa.Infrastructure.Multa.Repositories;
using Mre.Visas.Multa.Infrastructure.Persistence.Contexts;
using Mre.Visas.Multa.Infrastructure.Shared.Interfaces;
using Mre.Visas.Multa.Infrastructure.Shared.Repositories;

namespace Mre.Visas.Multa.Infrastructure
{
    public static class ServiceRegistrations
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"),
                options => options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IMultaRepository, MultaRepository>();
        }
    }
}