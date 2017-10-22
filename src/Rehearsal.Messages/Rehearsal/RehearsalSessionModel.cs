using System;
using System.Collections.Generic;

namespace Rehearsal.Messages.Rehearsal
{
    public class RehearsalSessionModel
    {
        public Guid Id { get; set; }
        public ICollection<RehearsalQuestionModel> Questions { get; set; }
    }
}