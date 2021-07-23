using System;

namespace MessageApprover.Saga.Messages
{
    public interface ISagaMessage
    {
        Guid CorrelationId { get; }

        Guid Id { get; }

        Guid AuthorId { get; }

        string Text { get; }
    }
}
