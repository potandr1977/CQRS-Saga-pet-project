using MessageApprover.Domain;
using MessageApprover.Queries.DataAccess.Mongo.Abstractions;
using MessageApprover.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace MessageApprover.Queries.DataAccess.Mongo
{
    public class AuthorDao : IAuthorDao
    {
        readonly IMongoDatabase database;

        public AuthorDao()
        {
            MongoClient client = new MongoClient(MongoSettings.ConnectionString);
            database = client.GetDatabase(MongoSettings.DbName);
        }

        private IMongoCollection<Author> Authors
        {
            get
            {
                return database.GetCollection<Author>(MongoSettings.AuthorsCollectionName);
            }
        }

        public Task<Author> GetAuthorById(Guid authorId)
        {
            return Authors.Find(doc=> doc.Id == authorId).FirstOrDefaultAsync();
        }
    }
}
