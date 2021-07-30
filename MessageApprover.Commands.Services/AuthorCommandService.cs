using MessageApprover.Commands.DataAccess.Abstractions;
using MessageApprover.Commands.Services.Abstractions;
using MessageApprover.Domain;
using System.Threading.Tasks;

namespace MessageApprover.Commands.Services
{
    public class AuthorCommandService : IAuthorCommandService
    {
        private readonly IAuthorDao authorDao;

        public AuthorCommandService(IAuthorDao authorDao)
        {
            this.authorDao = authorDao;
        }

        public Task Save(Author author)
        {
            return authorDao.Save(author);
        }
    }
}
