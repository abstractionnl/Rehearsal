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
            var correctAnswer = Question.AvailableAnswers[Question.CorrectAnswer];
            var isCorrect = correctAnswer == answer;
                
            return Task.FromResult(new AnswerResultModel()
            {
                QuestionId = Question.Id,
                CorrectAnswers = new[] { correctAnswer },
                GivenAnswer = answer,
                IsCorrect = isCorrect
            });

        }
    }
}