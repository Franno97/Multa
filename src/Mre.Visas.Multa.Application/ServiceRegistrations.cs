using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Mre.Visas.Multa.Application.Behaviors;
using System.Reflection;

namespace Mre.Visas.Multa.Application
{
    public static class ServiceRegistrations
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}