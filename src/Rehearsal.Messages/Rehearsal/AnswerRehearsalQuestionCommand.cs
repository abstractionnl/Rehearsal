using System;
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.Rehearsal
{
    public class AnswerRehearsalQuestionCommand : BaseCommand
    {
        public Guid QuestionId { get; set; }
        public AnswerResultModel Result { get; set; }
    }
}