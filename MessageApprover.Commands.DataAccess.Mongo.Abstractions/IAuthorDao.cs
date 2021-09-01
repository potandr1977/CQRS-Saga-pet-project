using MessageApprover.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageApprover.Commands.DataAccess.Abstractions
{
    public interface IAuthorDao
    {
        Task<List<Author>> GetAll();

        Task<Author> GetById(Guid id);

        Task Save(Author author);
    }
}
