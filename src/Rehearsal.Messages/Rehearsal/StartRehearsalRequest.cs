using System;

namespace Rehearsal.Messages.Rehearsal
{
    public class StartRehearsalRequest
    {
        public Guid QuestionListId { get; set; }
        public RehearsalQuestionType QuestionType { get; set; }
    }
}