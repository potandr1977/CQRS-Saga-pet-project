using MessageApprover.Commands.Abstractions;
using MessageApprover.CommandsAbstractions.Author;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MessageApprover.Saga.Messages.EnteredMessages;
using System;
using MessageApprover.Settings;

namespace MessageApprover.Commands.handlers
{
    public class CreateEnteredMessageCommandHandler : ICommandHandler<CreateEnteredMessageCommand>
    {
        readonly IBusControl busControl;

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
            await busControl.Publish<IStartApprovementCommand>(new
            {
                Id = command.Id,
                AuthorId = command.AuthorId,
                Text = command.Text
            });
        }
    }
}
