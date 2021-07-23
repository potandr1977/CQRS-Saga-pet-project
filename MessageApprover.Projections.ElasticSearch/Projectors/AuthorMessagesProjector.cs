using MessageApprover.Data.Elastic;
using MessageApprover.Queries.DataAccess.Elastic.Abstractions;
using MessageApprover.Queries.DataAccess.Mongo.Abstractions;
using MessageApprover.Saga.Messages.EnteredMessages;
using System.Threading.Tasks;

namespace MessageApprover.Projections.ElasticSearch
{
    public class AuthorMessagesProjector : IAuthorMessagesProjector
    {
        readonly IAuthorDao authorDao;
        readonly IAuthorMessagesDao authorMessagesDao;

        public AuthorMessagesProjector(IAuthorMessagesDao authorMessagesDao, IAuthorDao authorDao)
        {
            this.authorDao = authorDao;
            this.authorMessagesDao = authorMessagesDao;
        }

        public async Task Project(IMessageReadyForProjection message)
        {
            var author = await authorDao.GetAuthorById(message.AuthorId);
            var authorMessage = new AuthorMessage
            {
                Id = message.Id,
                AuthorId = message.AuthorId,
                AuthorName = author.Name,
                MessageText = message.Text
            };

            await authorMessagesDao.Save(authorMessage);
        }
    }
}
