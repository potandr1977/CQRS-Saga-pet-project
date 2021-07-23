using Automatonymous;
using System;

namespace MessageApprover.Sagas
{
    public class ApprovementState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? DeclineDate { get; set; }

        public string CurrentState { get; set; }

        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public string Text { get; set; }
    }
}
