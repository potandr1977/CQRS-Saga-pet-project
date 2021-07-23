using MessageApprover.Saga.Messages.EnteredMessages;
using System.Threading.Tasks;

namespace MessageApprover.Projections.ElasticSearch
{
    public interface IAuthorMessagesProjector
    {
        Task Project(IMessageReadyForProjection message);
    }
}
