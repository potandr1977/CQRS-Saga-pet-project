using System;

namespace MessageApprover.Saga.Messages.EnteredMessages
{
    public interface IStartApprovementCommand
    {
        Guid Id { get; set; }

        Guid AuthorId { get; set; }

        string Text { get; set; }
    }

    
}
