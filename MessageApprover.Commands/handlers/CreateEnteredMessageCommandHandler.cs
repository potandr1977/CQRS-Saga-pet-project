using MessageApprover.Commands.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MessageApprover.Saga.Messages.EnteredMessages;
using MessageApprover.Commands.Abstractions.EnteredMessage;

namespace MessageApprover.Commands.handlers
{
    public class CreateEnteredMessageCommandHandler : ICommandHandler<CreateEnteredMessageCommand>
    {
        private readonly IBusControl busControl;

        public CreateEnteredMessageCommandHandler(IBusControl busControl)
        {
            this.busControl = busControl;
        }

        public Task Handle(CreateEnteredMessageCommand command, CancellationToken cancellationToken)
        {
            return SendSagaCommand(command);
        }

        private async Task SendSagaCommand(CreateEnteredMessageCommand command)
        {
            /*
            var sendToUri = new Uri($"{RabbitSettings.RabbitMqUri}{RabbitSettings.SagaQueue}:{nameof(IStartApprovementCommand)}");
            var endPoint = await busControl.GetSendEndpoint(sendToUri);
            await endPoint.Send<IStartApprovementCommand>(new
            {
                Id = command.Id,
                AuthorId = command.AuthorId,
                Text = command.Text
            });
            */
            
            await busControl.Publish<IStartApprovementCommand>(new
            {
                Id = command.Id,
                AuthorId = command.AuthorId,
                Text = command.Text
            });
            
        }
    }
}
