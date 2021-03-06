using MassTransit;
using MessageApprover.Commands.Abstractions;
using MessageApprover.Commands.Abstractions.EnteredMessage;
using MessageApprover.Saga.Messages.EnteredMessages;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace MessageApprover.Commands.Api.Consumers
{
    public class WaitForApproveConsumer : IConsumer<WaitingForApproveStarted>
    {
        private readonly ICommandDispatcher commandDispatcher;

        public WaitForApproveConsumer(IServiceCollection services)
        {
            commandDispatcher = services.BuildServiceProvider().GetService<ICommandDispatcher>();
        }

        public async Task Consume(ConsumeContext<WaitingForApproveStarted> context)
        {
            //I wont to develop message approvement UI, that is why I'm going to save message text in mongoDB and 
            //publish saga message 
            
            await commandDispatcher.Send(new MessageApprovedCommand
            {
                Id = context.Message.Id,
                AuthorId = context.Message.AuthorId,
                Text = context.Message.Text
            });
            

            await context.Publish<IMessageApproved>(new  
            {
                CorrelationId = context.Message.CorrelationId,
                Id = context.Message.Id,
                AuthorId = context.Message.AuthorId,
                Text = context.Message.Text
            });

            //return context.Publish<IMessageDeclined>(context.Message);
        }
    }
    
}
