using MessageApprover.Commands.Abstractions;
using System;

namespace MessageApprover.CommandsAbstractions.Author
{
    public class CreateAuthorCommand : ICommand 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateEnteredMessageCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Text { get; set; }
    }

    public class MessageApprovedCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Text { get; set; }
    }
}
