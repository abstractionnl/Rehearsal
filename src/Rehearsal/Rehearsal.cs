using System;
using System.Collections.Generic;
using CQRSlite.Domain;
using Rehearsal.Messages;

namespace Rehearsal
{
    public class Rehearsal : AggregateRoot
    {
        [Obsolete("For CQRS")]
        private Rehearsal() {}

        public Rehearsal(Guid id, ICollection<QuestionModel> questions)
        {
            Id = id;
            
            ApplyChange(new RehearsalStartedEvent()
            {
                Questions = questions
            });
        }
    }
}