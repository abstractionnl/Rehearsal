using System;
using System.Collections.Generic;
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages
{
    public class StartRehearsalCommand : BaseCommand
    {
        public ICollection<QuestionModel> Questions { get; set; }
    }
}