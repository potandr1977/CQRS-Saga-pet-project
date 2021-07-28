using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApprover.Commands.Abstractions.EnteredMessage
{
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
