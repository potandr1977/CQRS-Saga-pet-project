using System.Threading;
using System.Threading.Tasks;

namespace MessageApprover.Commands.Abstractions
{
    public interface ICommandDispatcher
    {
        Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken))
            where TCommand : ICommand;

        Task<TResult> Send<TCommand, TResult>(TCommand command,
            CancellationToken cancellationToken = default(CancellationToken)) where TCommand : ICommand<TResult>;
    }
}
