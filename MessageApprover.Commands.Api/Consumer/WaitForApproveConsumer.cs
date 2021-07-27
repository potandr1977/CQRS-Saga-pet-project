using MassTransit;
using MessageApprover.Commands.Abstractions;
using MessageApprover.CommandsAbstractions.Author;
using MessageApprover.Saga.Messages.EnteredMessages;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace MessageApprover.Consumers
{

    public class WaitForApproveConsumer : IConsumer<WaitingForApproveStarted>
    {
        readonly ICommandDispatcher commandDispatcher;

        public WaitForApproveConsumer(IServiceCollection services)
        {
            commandDispatcher = services.BuildServiceProvider().GetService<ICommandDispatcher>();
        }

        public async Task Consume(ConsumeContext<WaitingForApproveStarted> context)
        {
            //тут организуем процесс предоставления значения на утверждение специалистом, 
            //например складываем введённое значение в отдельную БД или отсылаем в другой микросервис
            //в микросервисе куда мы отослали будет опубликовано либо сообщение MessageApproved или MessageDecliened
            //для упрощения примера опубликуем ообщение в этом методе
            
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
