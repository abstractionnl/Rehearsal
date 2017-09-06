using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages
{
    public class QuestionListCreatedEvent : BaseEvent
    {
        public QuestionListProperties QuestionList { get; set; }
    }

    public class QuestionListUpdateEvent : BaseEvent
    {
        public QuestionListProperties QuestionList { get; set; }
    }
}