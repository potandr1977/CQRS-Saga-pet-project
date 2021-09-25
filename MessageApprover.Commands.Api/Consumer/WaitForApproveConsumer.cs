using MassTransit;
using MediatR;
using MessageApprover.Commands.Abstractions.EnteredMessage;
using MessageApprover.Saga.Messages.EnteredMessages;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace MessageApprover.Commands.Api.Consumers
{
    public class WaitForApproveConsumer : IConsumer<WaitingForApproveStarted>
    {
        private readonly IMediator mediator;

        public WaitForApproveConsumer(IServiceCollection services)
        {
            mediator = services.BuildServiceProvider().GetService<IMediator>();
        }

        public Task Consume(ConsumeContext<WaitingForApproveStarted> context)
        {
            if (context is null)
            {
                throw new System.ArgumentNullException(nameof(context));
            }
            //I wont to develop message approvement UI, that is why I'm going to save message text in mongoDB and 
            //publish saga message 

            var messageTask = context.Publish<IMessageApproved>(new
            {
                CorrelationId = context.Message.CorrelationId,
                Id = context.Message.Id,
                AuthorId = context.Message.AuthorId,
                Text = context.Message.Text
            });


            var saveTask =  mediator.Send(new MessageApprovedCommand
            {
                Id = context.Message.Id,
                AuthorId = context.Message.AuthorId,
                Text = context.Message.Text
            });

            return Task.WhenAll(messageTask,saveTask);
            
            //return context.Publish<IMessageDeclined>(context.Message);
        }
    }
    
}
