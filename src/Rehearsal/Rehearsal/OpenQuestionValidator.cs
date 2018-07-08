using System.Threading.Tasks;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class OpenQuestionValidator : IAnswerValidator
    {
        public OpenQuestionValidator(OpenQuestionModel question)
        {
            Question = question;
        }

        private OpenQuestionModel Question { get; }
        
        public Task<AnswerResultModel> Validate(string answer)
        {
            var isCorrect = Question.CorrectAnswers.Contains(answer);
            
            return Task.FromResult(new AnswerResultModel()
            {
                QuestionId = Question.Id,
                CorrectAnswers = Question.CorrectAnswers,
                GivenAnswer = answer,
                IsCorrect = isCorrect
            });
        }
    }
}