using MessageApprover.Queries.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessageApprover.Queries.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorMessagesController : ControllerBase
    {
        readonly IAuthorMessagesQueryService authorMessagesQueryService;

        public AuthorMessagesController(IAuthorMessagesQueryService authorMessagesQueryService)
        {
            this.authorMessagesQueryService = authorMessagesQueryService;
        }
        
        [HttpGet("{id}")]
        public async Task<IReadOnlyCollection<GetAuthorMessages.ResultItem>> Get(string id)
        {
            var messages = await authorMessagesQueryService.GetAuthorMessagesByAuthorId(new GetAuthorMessages { AuthorId = id });

            return messages;
        }
        
    }
}
