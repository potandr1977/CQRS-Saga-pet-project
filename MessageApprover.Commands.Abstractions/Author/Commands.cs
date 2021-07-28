using MessageApprover.Commands.Abstractions;
using System;

namespace MessageApprover.CommandsAbstractions.Author
{
    public class CreateAuthorCommand : ICommand 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
