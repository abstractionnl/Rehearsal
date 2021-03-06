﻿using System.Collections.Generic;
using Rehearsal.Messages.Infrastructure;
using Rehearsal.Messages.QuestionList;

namespace Rehearsal.Messages.Rehearsal
{
    [EventName("RehearsalStartedEvent")]
    public class RehearsalStartedEvent : BaseEvent
    {
        public ICollection<RehearsalQuestionModel> Questions { get; set; }
    }
}