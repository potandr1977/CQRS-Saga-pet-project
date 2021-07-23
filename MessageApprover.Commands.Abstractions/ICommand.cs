namespace MessageApprover.Commands.Abstractions
{
    public interface ICommand
    {
    }

    public interface ICommand<TResult> : ICommand
    {
    }
}
