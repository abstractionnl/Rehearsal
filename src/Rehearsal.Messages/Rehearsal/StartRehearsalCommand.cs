using System;
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.Rehearsal
{
    public class StartRehearsalCommand : BaseCommand
    {
        public Guid QuestionListId { get; set; }
        public RehearsalQuestionType QuestionType { get; set; }
    }
}