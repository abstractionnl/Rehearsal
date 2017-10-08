using System.Collections.Generic;
using Rehearsal.Messages.Infrastructure;
using Rehearsal.Messages.QuestionList;

namespace Rehearsal.Messages.Rehearsal
{
    public class StartRehearsalCommand : BaseCommand
    {
        public ICollection<QuestionModel> Questions { get; set; }
    }
}