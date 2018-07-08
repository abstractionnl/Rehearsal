using System;
using System.Threading.Tasks;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class MultipleChoiceQuestionValidator : IAnswerValidator
    {
        public MultipleChoiceQuestionValidator(MultipleChoiceQuestionModel question)
        {
            Question = question;
        }
        
        private MultipleChoiceQuestionModel Question { get; }
        
        public Task<AnswerResultModel> Validate(string answer)
        {
            if (!int.TryParse(answer, out int answerAsInt))
                throw new InvalidOperationException($"Answer '{answer}' could not be parsed as an integer");
            
            var isCorrect = Question.CorrectAnswer == answerAsInt;
                
            return Task.FromResult(new AnswerResultModel()
            {
                QuestionId = Question.Id,
                CorrectAnswers = new[] { Question.CorrectAnswer.ToString() },
                GivenAnswer = answer,
                IsCorrect = isCorrect
            });

        }
    }
}