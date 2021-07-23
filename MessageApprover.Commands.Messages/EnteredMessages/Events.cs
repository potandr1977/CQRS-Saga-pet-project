
namespace MessageApprover.Saga.Messages.EnteredMessages
{
    public interface IMessageWaitsForApprove : ISagaMessage
    {
    }

    public interface IMessageDeclined : ISagaMessage
    {
    }

    public interface IMessageApproved : ISagaMessage
    {
    }

    public interface IMessageReadyForProjection : ISagaMessage
    {
    }
}
