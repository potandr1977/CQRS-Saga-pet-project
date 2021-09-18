using MediatR;
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
        private readonly IMediator mediator;

        public AuthorController(IMediator mediator)
        {
            this.mediator = mediator;
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

            await mediator.Send(createAuthorCommand);

            return id.ToString();
        }
    }
}
