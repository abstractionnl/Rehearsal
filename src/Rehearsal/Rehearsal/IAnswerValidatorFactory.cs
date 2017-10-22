using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public interface IAnswerValidatorFactory
    {
        IAnswerValidator GetValidatorFor(RehearsalQuestionModel questionModel);
    }
}