using System;

namespace MessageApprover.Data.Elastic
{
    public class AuthorMessage
    {
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string MessageText { get; set; }
    }
}
