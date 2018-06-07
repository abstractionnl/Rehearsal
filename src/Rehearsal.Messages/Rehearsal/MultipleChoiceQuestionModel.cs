using System;
using System.Collections.Generic;

namespace Rehearsal.Messages.Rehearsal
{
    public class MultipleChoiceQuestionModel : RehearsalQuestionModel
    {
        public IList<string> AvailableAnswers { get; set; }
        public int CorrectAnswer { get; set; }
    }
}