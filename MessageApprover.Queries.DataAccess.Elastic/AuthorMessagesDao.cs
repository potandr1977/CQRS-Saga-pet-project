using MessageApprover.Data.Elastic;
using MessageApprover.Queries.DataAccess.Elastic.Abstractions;
using MessageApprover.Settings;
using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageApprover.Queries.DataAccess.Elastic
{
    public class AuthorMessagesDao : IAuthorMessagesDao
    {
        private readonly IElasticClient elasticClient;

        public AuthorMessagesDao(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public Task Save(AuthorMessage authorMessage)
        {
            return elasticClient.IndexDocumentAsync(authorMessage);
        }

        public async Task<IReadOnlyCollection<GetAuthorMessages.ResultItem>> GetAuthorMessages(GetAuthorMessages query)
        {
            var resp = await elasticClient.SearchAsync<GetAuthorMessages.ResultItem>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                     .Match(m => m
                        .Field(f => f.AuthorId)
                        .Query(query.AuthorId)
                     )
                )
            );

            return resp.Documents;
        }

        public async Task<IReadOnlyCollection<GetAllMessages.ResultItem>> GetAllMessages(GetAllMessages query)
        {
            //returns all messages for this moment, autocompletion search will be added later

            var resp = await elasticClient.SearchAsync<GetAllMessages.ResultItem>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .MatchAll()
                )
            );

            return resp.Documents;
        }
    }
}
