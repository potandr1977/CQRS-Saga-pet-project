using MassTransit;
using MessageApprover.Commands.DataAccess.Abstractions;
using MessageApprover.Commands.Services.Abstractions;
using MessageApprover.Domain;
using MessageApprover.Saga.Messages.EnteredMessages;
using System.Threading.Tasks;

namespace MessageApprover.Commands.Services
{
    public class EnteredMessagesCommandService : IEnteredMessagesCommandService
    {
        private readonly IEnteredMessageDao enteredMessageDao;
        private readonly IBusControl busControl;

        public EnteredMessagesCommandService(IEnteredMessageDao enteredMessageDao, IBusControl busControl)
        {
            this.enteredMessageDao = enteredMessageDao;
            this.busControl = busControl;
        }

        public async Task SaveMessage(EnteredMessage message)
        {
            if (message is null)
            {
                throw new System.ArgumentNullException(nameof(message));
            }

            await enteredMessageDao.Save(message);

            await busControl.Publish<IMessageReadyForProjection>(new
            {
                message.Id,
                message.AuthorId,
                message.Text
            });
        }
    }
}
