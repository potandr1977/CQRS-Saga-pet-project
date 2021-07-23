using MessageApprover.Data.Elastic;
using System;

namespace MessageApprover.Queries
{
    public class GetAuthorMessages
    {
        public string AuthorId { get; set; }

        public class ResultItem : AuthorMessage {};
    }

    public class GetAllMessages
    {
        public string AuthorName { get; set; }

        public string MessageText { get; set; }

        public class ResultItem : AuthorMessage {};
    }
}
