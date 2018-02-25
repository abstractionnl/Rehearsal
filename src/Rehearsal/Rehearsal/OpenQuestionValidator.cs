using System.Threading.Tasks;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class OpenQuestionValidator : IAnswerValidator
    {
        public OpenQuestionValidator(OpenRehearsalQuestionModel question)
        {
            Question = question;
        }

        private OpenRehearsalQuestionModel Question { get; }
        
        public Task<AnswerResultModel> Validate(string answer)
        {
            var isCorrect = Question.CorrectAnswers.Contains(answer);
            
            return Task.FromResult(new AnswerResultModel()
            {
                CorrectAnswers = Question.CorrectAnswers,
                GivenAnswer = answer,
                IsCorrect = isCorrect
            });
        }
    }
}