using MessageApprover.Commands.Abstractions;
using MessageApprover.CommandsAbstractions.Author;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessageApprover.Commands.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;

        public AuthorController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<string> When(string authorNameToCreate)
        {
            var id = Guid.NewGuid();
            var createAuthorCommand = new CreateAuthorCommand 
            { 
                Id =  id,
                Name = authorNameToCreate
            };

            await commandDispatcher.Send(createAuthorCommand);

            return id.ToString();
        }
    }
}
