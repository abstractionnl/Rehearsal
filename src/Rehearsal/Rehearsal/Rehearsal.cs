using System;
using System.Collections.Generic;
using CQRSlite.Domain;
using Rehearsal.Messages;
using Rehearsal.Messages.QuestionList;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
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