using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class RehearsalSession : IRehearsalSession
    {
        private ICollection<RehearsalQuestionModel> Questions { get; }
        private IAnswerValidatorFactory ValidatorFactory { get; }

        public RehearsalSession(ICollection<RehearsalQuestionModel> questions, IAnswerValidatorFactory validatorFactory)
        {
            ValidatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
            Questions = questions ?? throw new ArgumentNullException(nameof(questions));
        }

        public Task<AnswerResultModel> GiveAnswer(Guid questionId, string answer)
        {
            var question = Questions.Where(x => x.Id == questionId).HeadOrNone()
                .IfNone(() => throw new InvalidOperationException("invalid question id"));

            return ValidatorFactory.GetValidatorFor(question).Validate(answer);
        }
    }
}