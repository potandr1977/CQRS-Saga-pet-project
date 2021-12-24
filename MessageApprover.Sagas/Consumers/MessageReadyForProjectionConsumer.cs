using MassTransit;
using MessageApprover.Projections.ElasticSearch;
using MessageApprover.Saga.Messages.EnteredMessages;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace MessageApprover.Consumers
{
    public class MessageReadyForProjectionConsumer : IConsumer<IMessageReadyForProjection>
    {
        private readonly IAuthorMessagesProjector authorMessageProjector;

        public MessageReadyForProjectionConsumer(IServiceCollection services)
        {
            authorMessageProjector = services.BuildServiceProvider().GetService<IAuthorMessagesProjector>();
        }

        public Task Consume(ConsumeContext<IMessageReadyForProjection> context) => 
            authorMessageProjector.Project(context.Message);
    }
}
