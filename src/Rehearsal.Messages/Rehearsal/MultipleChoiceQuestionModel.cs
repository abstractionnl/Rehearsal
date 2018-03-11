using System;
using System.Collections.Generic;

namespace Rehearsal.Messages.Rehearsal
{
    public class MultipleChoiceQuestionModel : RehearsalQuestionModel
    {
        public ICollection<string> AvailableAnswers { get; set; }
        public int CorrectAnswer { get; set; }
    }
}