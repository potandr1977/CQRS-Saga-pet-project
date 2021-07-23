using MessageApprover.Data.Elastic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageApprover.Queries.DataAccess.Elastic.Abstractions
{
    public interface IAuthorMessagesDao
    {
        Task<IReadOnlyCollection<GetAuthorMessages.ResultItem>> GetAuthorMessages(GetAuthorMessages query);

        Task<IReadOnlyCollection<GetAllMessages.ResultItem>> GetAllMessages(GetAllMessages query);

        Task Save(AuthorMessage authorMessage);
    }
}
