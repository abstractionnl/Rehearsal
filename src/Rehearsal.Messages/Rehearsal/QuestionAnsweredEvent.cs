using System;
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.Rehearsal
{
    [EventName("QuestionAnsweredEvent")]
    public class QuestionAnsweredEvent : BaseEvent
    {
        public Guid QuestionId { get; set; }
        public string GivenAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}