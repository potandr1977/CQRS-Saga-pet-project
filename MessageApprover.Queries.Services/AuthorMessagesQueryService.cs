using MessageApprover.Queries.Abstractions;
using MessageApprover.Queries.DataAccess.Elastic.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageApprover.Queries
{
    public class AuthorMessagesQueryService : IAuthorMessagesQueryService
    {
        readonly IAuthorMessagesDao authorMessagesDao;

        public AuthorMessagesQueryService (IAuthorMessagesDao authorMessagesDao)
        {
            this.authorMessagesDao = authorMessagesDao;
        }

        public Task<IReadOnlyCollection<GetAllMessages.ResultItem>> GetAllMessages(GetAllMessages query)
        {
            return authorMessagesDao.GetAllMessages(query);
        }

        Task<IReadOnlyCollection<GetAuthorMessages.ResultItem>> IAuthorMessagesQueryService.GetAuthorMessagesByAuthorId(GetAuthorMessages query)
        {
            return authorMessagesDao.GetAuthorMessages(query);
        }
    }
}
