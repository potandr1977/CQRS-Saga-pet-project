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
        readonly IMongoDatabase database;

        public EnteredMessageDao()
        {
            MongoClient client = new MongoClient(MongoSettings.ConnectionString);
            database = client.GetDatabase(MongoSettings.DbName);
        }

        private IMongoCollection<EnteredMessage> EnteredMessages
        {
            get { 
                return database.GetCollection<EnteredMessage>(MongoSettings.EnteredMessagesCollectionName); 
            }
        }

        public async Task<IReadOnlyList<EnteredMessage>> GetAll()
        {
            var builder = new FilterDefinitionBuilder<EnteredMessage>();
            var filter = builder.Empty;

            return await EnteredMessages.Find(filter).ToListAsync();
        }

        public async Task<IReadOnlyList<EnteredMessage>> GetByAutorId(Guid authorId)
        {
            var builder = new FilterDefinitionBuilder<EnteredMessage>();
            var filter = builder.Empty;

            return await EnteredMessages.Find(filter).ToListAsync();
        }

        public async Task<EnteredMessage> GetById(Guid id)
        {
            return await EnteredMessages.Find(new BsonDocument("Id", id.ToByteArray())).FirstOrDefaultAsync();
        }

        public async Task Save(EnteredMessage message)
        {
            await EnteredMessages.InsertOneAsync(message);
        }
    }
}
