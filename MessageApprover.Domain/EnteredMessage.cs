using System;

namespace MessageApprover.Domain
{
    public class EnteredMessage
    {
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public string Text { get; set; }
    }
}
