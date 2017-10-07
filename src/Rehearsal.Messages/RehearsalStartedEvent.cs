using System.Collections.Generic;
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages
{
    public class RehearsalStartedEvent : BaseEvent
    {
        public ICollection<QuestionModel> Questions { get; set; }
    }
}