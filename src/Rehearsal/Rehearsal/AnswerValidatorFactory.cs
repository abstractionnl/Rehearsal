using System;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class AnswerValidatorFactory : IAnswerValidatorFactory
    {
        public IAnswerValidator GetValidatorFor(RehearsalQuestionModel questionModel)
        {
            if (questionModel is OpenRehearsalQuestionModel o)
                return new OpenQuestionValidator(o);
            
            throw new InvalidOperationException($"Unsupported question type {questionModel.GetType().FullName}");
        }
    }
}