using System;

namespace MessageApprover.Saga.Messages.EnteredMessages
{
    public interface IStartApprovementCommand
    {
        Guid Id { get; set; }

        Guid AuthorId { get; set; }

        string Text { get; set; }
    }

    public class WaitForApproveCommand : ISagaMessage
    {
        public Guid CorrelationId { get; set; }

        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public string Text { get; set; }
    }
}
