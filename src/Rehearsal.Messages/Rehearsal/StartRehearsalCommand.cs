using System.Collections.Generic;
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.Rehearsal
{
    public class StartRehearsalCommand : BaseCommand
    {
        public ICollection<RehearsalQuestionModel> Questions { get; set; }
    }
}