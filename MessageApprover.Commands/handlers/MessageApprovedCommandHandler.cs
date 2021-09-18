using MessageApprover.Commands.Abstractions;
using MessageApprover.Commands.Mappers;
using System.Threading;
using System.Threading.Tasks;
using MessageApprover.Commands.Services.Abstractions;
using MessageApprover.Commands.Abstractions.EnteredMessage;
using MediatR;

namespace MessageApprover.Commands.handlers
{
    public class MessageApprovedCommandHandler : IRequestHandler<MessageApprovedCommand, NullResponse>
    {
        private readonly IEnteredMessagesCommandService enteredMessagesCommandService;

        public MessageApprovedCommandHandler(IEnteredMessagesCommandService enteredMessagesCommandService)
        {
            this.enteredMessagesCommandService = enteredMessagesCommandService;
        }

        public async Task<NullResponse> Handle(MessageApprovedCommand command, CancellationToken cancellationToken)
        {
            var message = command.ToDomain();

            await enteredMessagesCommandService.SaveMessage(message);

            return new NullResponse();
        }
    }
}
