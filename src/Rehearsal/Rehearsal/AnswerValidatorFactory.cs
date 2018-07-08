using System;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class AnswerValidatorFactory : IAnswerValidatorFactory
    {
        public IAnswerValidator GetValidatorFor(RehearsalQuestionModel questionModel)
        {
            switch (questionModel)
            {
                case OpenQuestionModel o:
                    return new OpenQuestionValidator(o);
                case MultipleChoiceQuestionModel mc:
                    return new MultipleChoiceQuestionValidator(mc);
            }

            throw new InvalidOperationException($"Unsupported question type {questionModel.GetType().FullName}");
        }
    }
}