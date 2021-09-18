using MediatR;
using System;

namespace MessageApprover.Commands.Abstractions.EnteredMessage
{
    public class CreateEnteredMessageCommand : IRequest<NullResponse>
    {
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public string Text { get; set; }
    }

    public class MessageApprovedCommand : IRequest<NullResponse>
    {
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public string Text { get; set; }
    }
}
