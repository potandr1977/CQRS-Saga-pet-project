using MessageApprover.Commands.DataAccess.Abstractions;
using MessageApprover.Domain;
using MessageApprover.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageApprover.Commands.DataAccess
{
    public class EnteredMessageDao : IEnteredMessageDao
    {
        private readonly IMongoDatabase database;

        public EnteredMessageDao(IMongoClient mongoClient)
        {
            database = mongoClient.GetDatabase(MongoSettings.DbName);
        }

        private IMongoCollection<EnteredMessage> EnteredMessages => database.GetCollection<EnteredMessage>(MongoSettings.EnteredMessagesCollectionName);

        public Task<List<EnteredMessage>> GetAll()
        {
            var builder = new FilterDefinitionBuilder<EnteredMessage>();
            var filter = builder.Empty;

            return EnteredMessages.Find(filter).ToListAsync();
        }

        public Task<List<EnteredMessage>> GetByAuthorId(Guid authorId)
        {
            var builder = new FilterDefinitionBuilder<EnteredMessage>();
            var filter = builder.Empty;

            return EnteredMessages.Find(filter).ToListAsync();
        }

        public  Task<EnteredMessage> GetById(Guid id)
        {
            return EnteredMessages.Find(new BsonDocument("Id", id.ToByteArray())).FirstOrDefaultAsync();
        }

        public Task Save(EnteredMessage message)
        {
            return EnteredMessages.InsertOneAsync(message);
        }
    }
}
