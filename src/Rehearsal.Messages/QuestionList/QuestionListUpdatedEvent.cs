
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.QuestionList
{
    [EventName("QuestionListUpdatedEvent")]
    public class QuestionListUpdatedEvent : BaseEvent
    {
        public QuestionListProperties QuestionList { get; set; }
    }
}