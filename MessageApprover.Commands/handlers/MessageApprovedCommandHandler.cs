using MessageApprover.Commands.Abstractions;
using MessageApprover.CommandsAbstractions.Author;
using MessageApprover.Commands.Mappers;
using System.Threading;
using System.Threading.Tasks;
using MessageApprover.Commands.Services.Abstractions;

namespace MessageApprover.Commands.handlers
{
    public class MessageApprovedCommandHandler : ICommandHandler<MessageApprovedCommand>
    {
        readonly IEnteredMessagesCommandService enteredMessagesCommandService;

        public MessageApprovedCommandHandler(IEnteredMessagesCommandService enteredMessagesCommandService)
        {
            this.enteredMessagesCommandService = enteredMessagesCommandService;
        }

        public Task Handle(MessageApprovedCommand command, CancellationToken cancellationToken)
        {
            var message = command.ToDomain();

            return enteredMessagesCommandService.SaveMessage(message);
        }
    }
}
