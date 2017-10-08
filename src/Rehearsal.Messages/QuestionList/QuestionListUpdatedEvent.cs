
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.QuestionList
{
    public class QuestionListUpdatedEvent : BaseEvent
    {
        public QuestionListProperties QuestionList { get; set; }
    }
}