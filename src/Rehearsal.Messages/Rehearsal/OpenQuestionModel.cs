using System.Collections.Generic;

namespace Rehearsal.Messages.Rehearsal
{
    public class OpenQuestionModel : RehearsalQuestionModel
    {
        public ICollection<string> CorrectAnswers { get; set; }
    }
}