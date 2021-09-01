using MessageApprover.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageApprover.Commands.DataAccess.Abstractions
{
    public interface IEnteredMessageDao
    {
        Task<List<EnteredMessage>> GetAll();

        Task<List<EnteredMessage>> GetByAuthorId(Guid authorId);

        Task<EnteredMessage> GetById(Guid id);

        Task Save(EnteredMessage message);
    }
}
