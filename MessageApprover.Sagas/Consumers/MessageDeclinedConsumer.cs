using MassTransit;
using MessageApprover.Saga.Messages.EnteredMessages;
using System.Threading.Tasks;

namespace MessageApprover.Consumers
{

    public class MessageDeclinedConsumer : IConsumer<IMessageDeclined>
    {
        public Task Consume(ConsumeContext<IMessageDeclined> context)
        {
            return Task.CompletedTask;
        }
    }
}
