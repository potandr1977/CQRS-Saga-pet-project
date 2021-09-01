
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
    public class AuthorDao : IAuthorDao
    {
        private readonly IMongoDatabase database;

        public AuthorDao(IMongoClient mongoClient)
        {
            database = mongoClient.GetDatabase(MongoSettings.DbName);
        }

        private IMongoCollection<Author> Authors => database.GetCollection<Author>(MongoSettings.AuthorsCollectionName);

        public Task Save(Author author)
        {
            return Authors.InsertOneAsync(author);
        }

        public Task<List<Author>> GetAll()
        {
            var builder = new FilterDefinitionBuilder<Author>();
            var filter = builder.Empty;

            return Authors.Find(filter).ToListAsync();
        }

        public Task<Author> GetById(Guid id)
        {
            return Authors.Find(new BsonDocument("Id", id.ToByteArray())).FirstOrDefaultAsync();
        }
    }
}
