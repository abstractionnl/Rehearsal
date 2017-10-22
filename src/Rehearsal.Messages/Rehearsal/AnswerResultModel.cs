using System;

namespace Rehearsal.Messages.Rehearsal
{
    public class AnswerResultModel
    {
        public string GivenAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}