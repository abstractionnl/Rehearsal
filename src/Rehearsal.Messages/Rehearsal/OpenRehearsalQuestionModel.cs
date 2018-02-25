using System.Collections.Generic;

namespace Rehearsal.Messages.Rehearsal
{
    public class OpenRehearsalQuestionModel : RehearsalQuestionModel
    {
        public ICollection<string> CorrectAnswers { get; set; }
    }
}