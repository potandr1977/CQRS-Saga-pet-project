﻿
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

        public async Task Save(Author author)
        {
            await Authors.InsertOneAsync(author);
        }

        public async Task<IReadOnlyList<Author>> GetAll()
        {
            var builder = new FilterDefinitionBuilder<Author>();
            var filter = builder.Empty; 

            return await Authors.Find(filter).ToListAsync();
        }

        public async Task<Author> GetById(Guid id)
        {
            return await Authors.Find(new BsonDocument("Id", id.ToByteArray())).FirstOrDefaultAsync();
        }
    }
}
