using System;
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.Rehearsal
{
    public class QuestionAnsweredEvent : BaseEvent
    {
        public Guid QuestionId { get; set; }
        public AnswerResultModel Result { get; set; }
    }
}