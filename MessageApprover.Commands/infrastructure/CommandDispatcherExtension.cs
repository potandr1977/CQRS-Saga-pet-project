using MessageApprover.Commands.Abstractions;
using MessageApprover.Commands.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace MessageApprover.Commands.infrastructure
{
    public static class CommandDispatcherExtension
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(item => item.GetInterfaces()
            .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)) && !item.IsAbstract && !item.IsInterface)
            .ToList()
            .ForEach(assignedTypes =>
            {
                var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(ICommandHandler<>));
                services.AddSingleton(serviceType, assignedTypes);
            });

            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

            return services;
        }
    }
}
