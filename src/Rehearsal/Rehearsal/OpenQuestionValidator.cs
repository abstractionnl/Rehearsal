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
            var isCorrect = Question.CorrectAnswer == answer;
            
            return Task.FromResult(new AnswerResultModel()
            {
                CorrectAnswer = Question.CorrectAnswer,
                GivenAnswer = answer,
                IsCorrect = isCorrect
            });
        }
    }
}