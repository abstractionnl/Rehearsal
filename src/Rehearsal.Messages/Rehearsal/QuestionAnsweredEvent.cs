using System;
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.Rehearsal
{
    [EventName("QuestionAnsweredEvent")]
    public class QuestionAnsweredEvent : BaseEvent
    {
        public Guid QuestionId { get; set; }
        public AnswerResultModel Result { get; set; }
    }
}