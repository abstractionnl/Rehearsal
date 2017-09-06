
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages
{
    public class QuestionListUpdatedEvent : BaseEvent
    {
        public QuestionListProperties QuestionList { get; set; }
    }
}