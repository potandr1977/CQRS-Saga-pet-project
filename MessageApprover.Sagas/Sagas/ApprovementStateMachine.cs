using Automatonymous;
using MessageApprover.Saga.Messages.EnteredMessages;
using System;

namespace MessageApprover.Sagas
{

    public class ApprovementStateMachine : MassTransitStateMachine<ApprovementState>
    {
        public State MessageWaitsForApproveState { get; private set; }
        public State MessageApprovedState { get; private set; }
        public State MessageDeclinedState { get; private set; }
        

        public Event<IStartApprovementCommand> StartApprovementCommand { get; set; }
        public Event<IMessageWaitsForApprove> MessageWaitsForApproveEvent { get; set; }
        public Event<IMessageApproved> MessageApprovedEvent { get; set; }
        public Event<IMessageDeclined> MessageDeclinedEvent { get; set; }

        public ApprovementStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => StartApprovementCommand, x => x.CorrelateBy(state => state.Id.ToString(), context => context.Message.Id.ToString()).SelectId(c => Guid.NewGuid()));
            Event(() => MessageWaitsForApproveEvent, x => x.CorrelateById(c => c.Message.CorrelationId));
            Event(() => MessageApprovedEvent, x => x.CorrelateById( c => c.Message.CorrelationId));
            Event(() => MessageDeclinedEvent, x => x.CorrelateById(c => c.Message.CorrelationId));

            During(Initial,
                When(StartApprovementCommand).Then(c => 
                {
                    c.Instance.Id = c.Data.Id;
                    c.Instance.AuthorId = c.Data.AuthorId;
                    c.Instance.Text = c.Data.Text;
                })
                .TransitionTo(MessageWaitsForApproveState)
                .Publish(ctx =>  new WaitingForApproveStarted() 
                { 
                    CorrelationId = ctx.Instance.CorrelationId, 
                    Id = ctx.Instance.Id,
                    AuthorId = ctx.Instance.AuthorId,
                    Text =  ctx.Instance.Text,
                }));

            During(MessageWaitsForApproveState,
                When(MessageApprovedEvent).Then(c =>
                {
                    c.Instance.CorrelationId = c.Data.CorrelationId;
                    c.Instance.Id = c.Data.Id;
                    c.Instance.AuthorId = c.Data.AuthorId;
                    c.Instance.Text = c.Data.Text;
                    c.Instance.SubmitDate = DateTime.Now;
                })
                .TransitionTo(MessageApprovedState)
                .Finalize()
                ,

                When(MessageDeclinedEvent).Then(c =>
                {
                    c.Instance.CorrelationId = c.Data.CorrelationId;
                    c.Instance.Id = c.Data.Id;
                    c.Instance.AuthorId = c.Data.AuthorId;
                    c.Instance.Text = c.Data.Text;
                    c.Instance.DeclineDate = DateTime.Now;
                })
                .TransitionTo(MessageDeclinedState)
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString()))
                .Finalize());;

            SetCompletedWhenFinalized();
        }
    }
}
