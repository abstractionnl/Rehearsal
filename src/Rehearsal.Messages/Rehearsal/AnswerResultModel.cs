using System;
using System.Collections.Generic;

namespace Rehearsal.Messages.Rehearsal
{
    public class AnswerResultModel
    {
        public string GivenAnswer { get; set; }
        public ICollection<string> CorrectAnswers { get; set; }
        public bool IsCorrect { get; set; }
    }
}