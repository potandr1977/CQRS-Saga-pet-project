using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageApprover.Queries.Abstractions
{
    public interface IAuthorMessagesQueryService
    {
        Task<IReadOnlyCollection<GetAuthorMessages.ResultItem>> GetAuthorMessagesByAuthorId(GetAuthorMessages query);

        Task<IReadOnlyCollection<GetAllMessages.ResultItem>> GetAllMessages(GetAllMessages query);
    }
}
