using MessageApprover.Domain;
using System.Threading.Tasks;

namespace MessageApprover.Commands.Services.Abstractions
{
    public interface IEnteredMessagesCommandService
    {
        Task SaveMessage(EnteredMessage message);
    }
}
