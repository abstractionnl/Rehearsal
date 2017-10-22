using System;
using System.Collections.Generic;
using Rehearsal.Messages.Infrastructure;
using Rehearsal.Messages.QuestionList;

namespace Rehearsal.Messages.Rehearsal
{
    public class RehearsalStartedEvent : BaseEvent
    {
        public ICollection<RehearsalQuestionModel> Questions { get; set; }
    }

    public class QuestionAnsweredEvent : BaseEvent
    {
        public Guid QuestionId { get; set; }
        public AnswerResultModel Result { get; set; }
    }
}