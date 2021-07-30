using MessageApprover.Commands.Abstractions;
using MessageApprover.Commands.Mappers;
using System.Threading;
using System.Threading.Tasks;
using MessageApprover.Commands.Services.Abstractions;
using MessageApprover.Commands.Abstractions.EnteredMessage;

namespace MessageApprover.Commands.handlers
{
    public class MessageApprovedCommandHandler : ICommandHandler<MessageApprovedCommand>
    {
        private readonly IEnteredMessagesCommandService enteredMessagesCommandService;

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
