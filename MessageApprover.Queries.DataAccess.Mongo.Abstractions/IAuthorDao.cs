using MessageApprover.Domain;
using System;
using System.Threading.Tasks;

namespace MessageApprover.Queries.DataAccess.Mongo.Abstractions
{
    public interface IAuthorDao
    {
        Task<Author> GetAuthorById(Guid authorId);
    }
}
