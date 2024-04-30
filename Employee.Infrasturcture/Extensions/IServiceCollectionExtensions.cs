using Employee.Domain.a_Common.Interfaces;
using Employee.Domain.a_Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace Employee.Infrasturcture.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services
            .AddTransient<IMediator, Mediator>()
                .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        }
    }
}
