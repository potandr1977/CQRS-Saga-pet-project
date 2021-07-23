using MessageApprover.Domain;
using System.Threading.Tasks;

namespace MessageApprover.Commands.Services.Abstractions
{
    public interface IAuthorCommandService
    {
        Task Save(Author author);
    }
}
