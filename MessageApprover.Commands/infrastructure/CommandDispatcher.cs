using MessageApprover.Commands.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessageApprover.Commands.Infrastructure
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken))
            where TCommand : ICommand
        {
            var commandНandler = (ICommandHandler<TCommand>) serviceProvider.GetService(typeof(ICommandHandler<TCommand>));

            return commandНandler.Handle(command, cancellationToken);
        }

        public Task<TResult> Send<TCommand, TResult>(TCommand command,
            CancellationToken cancellationToken = default(CancellationToken)) where TCommand : ICommand<TResult>
        {
            var commandНandler = (ICommandHandler<TCommand, TResult>)serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResult>));

            return commandНandler.Handle(command, cancellationToken);
        }
    }
}
