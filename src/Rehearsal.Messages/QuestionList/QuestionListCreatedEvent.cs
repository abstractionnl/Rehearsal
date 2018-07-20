using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.QuestionList
{
    [EventName("QuestionListCreatedEvent")]
    public class QuestionListCreatedEvent : BaseEvent
    {
        public QuestionListProperties QuestionList { get; set; }
    }
}