
using System;

namespace MessageApprover.Saga.Messages.EnteredMessages
{
    public class WaitingForApproveStarted : ISagaMessage
    {
        public Guid CorrelationId { get; set; }

        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public string Text { get; set; }
    }

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
