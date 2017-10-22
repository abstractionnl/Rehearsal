using System;

namespace Rehearsal.Messages.Rehearsal
{
    public class GiveAnswerRequest
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
    }
}