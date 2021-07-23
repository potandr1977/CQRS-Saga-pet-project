using MassTransit;
using MassTransit.Saga;
using MessageApprover.Consumers;
using MessageApprover.Projections.ElasticSearch;
using MessageApprover.Queries.DataAccess.Elastic;
using MessageApprover.Queries.DataAccess.Elastic.Abstractions;
using MessageApprover.Queries.DataAccess.Mongo;
using MessageApprover.Queries.DataAccess.Mongo.Abstractions;
using MessageApprover.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessageApprover.Sagas
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
            .AddSingleton<IAuthorDao, AuthorDao>()
            .AddSingleton<IAuthorMessagesDao, AuthorMessagesDao>()
            .AddSingleton<IAuthorMessagesProjector, AuthorMessagesProjector>();

            services.BuildServiceProvider();

            var sagaStateMachine = new ApprovementStateMachine();
            var repository = new InMemorySagaRepository<ApprovementState>();
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Durable = true;
                cfg.PrefetchCount = 1;
                cfg.PurgeOnStartup = true;
                cfg.Host(RabbitSettings.RabbitMqUri, hst =>
                {
                    hst.Username(RabbitSettings.UserName);
                    hst.Password(RabbitSettings.Password);
                });

                cfg.ReceiveEndpoint(RabbitSettings.SagaQueue, e =>
                {
                    e.Consumer(() => new MessageDeclinedConsumer());
                    e.Consumer(() => new MessageReadyForProjectionConsumer(services));

                    e.StateMachineSaga(sagaStateMachine, repository);
                });
            });

            await bus.StartAsync(CancellationToken.None);
            Console.WriteLine("Saga active.. Press enter to exit");
            Console.ReadLine();
            await bus.StopAsync(CancellationToken.None);

            Console.WriteLine("Saga inactive.. Press enter to exit");
        }
    }
}
