using MessageApprover.Commands.Abstractions;
using MessageApprover.CommandsAbstractions.Author;
using MessageApprover.Commands.Mappers;
using System.Threading;
using System.Threading.Tasks;
using MessageApprover.Commands.Services.Abstractions;
// ReSharper disable All

namespace MessageApprover.Commands.handlers
{
    public class CreateAuthroCommandHandler : ICommandHandler<CreateAuthorCommand>
    {
        readonly IAuthorCommandService authorCommandService;

        public CreateAuthroCommandHandler(IAuthorCommandService authorCommandService)
        {
            this.authorCommandService = authorCommandService;
        }

        public Task Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            var author = command.ToDomain();

            return authorCommandService.Save(author);
        }
    }
}
