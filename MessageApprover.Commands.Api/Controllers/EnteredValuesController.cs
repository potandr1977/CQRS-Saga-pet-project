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
    public class EnteredValuesController : ControllerBase
    {
        readonly ICommandDispatcher commandDispatcher;

        public EnteredValuesController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        // PUT api/<EnteredValuesController>/5
        [HttpPost("{authorId}")]
        public async Task<string> When(Guid authorId, string messageToCreate)
        {
            var id = Guid.NewGuid();
            var createEnteredMessageCommand = new CreateEnteredMessageCommand
            {
                Id = id,
                AuthorId = authorId,
                Text = messageToCreate
            };

            await commandDispatcher.Send(createEnteredMessageCommand);

            return id.ToString();
        }
    }
}
