using MessageApprover.Commands.Abstractions;
using MessageApprover.CommandsAbstractions.Author;
using MessageApprover.Commands.Mappers;
using System.Threading;
using System.Threading.Tasks;
using MessageApprover.Commands.Services.Abstractions;
using MediatR;
// ReSharper disable All

namespace MessageApprover.Commands.handlers
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, NullResponse>
    {
        private readonly IAuthorCommandService authorCommandService;

        public CreateAuthorCommandHandler(IAuthorCommandService authorCommandService)
        {
            this.authorCommandService = authorCommandService;
        }

        public async Task<NullResponse> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            var author = command.ToDomain();

            await authorCommandService.Save(author);

            return new NullResponse();
        }
    }
}
