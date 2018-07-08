using System.Linq;
using System.Threading.Tasks;
using NFluent;
using Rehearsal.Messages.Rehearsal;
using Rehearsal.Rehearsal;
using Xunit;

namespace Rehearsal.Tests.Rehearsal
{
    public abstract class OpenQuestionValidatorTests : TestBase
    {
        public class GivenValidator : OpenQuestionValidatorTests
        {
            private OpenQuestionModel Question { get; }
            private IAnswerValidator Validator { get; }
        
            public GivenValidator()
            {
                Question = Faker.OpenQuestion();
                Validator = new OpenQuestionValidator(Question);
            }

            [Fact]
            public async Task CanGiveCorrectAnswer()
            {
                var result = await Validator.Validate(Question.CorrectAnswers.First());

                Check.That(result.IsCorrect).IsTrue();
            }
            
            [Fact]
            public async Task OtherAnswerIsIncorrect()
            {
                var result = await Validator.Validate(Faker.Lorem.Sentence());

                Check.That(result.IsCorrect).IsFalse();
            }
        }

        public class GivenValidatorWithMultipleCorrectAnswers : OpenQuestionValidatorTests
        {
            private OpenQuestionModel Question { get; }
            private IAnswerValidator Validator { get; }
            
            public GivenValidatorWithMultipleCorrectAnswers()
            {
                Question = Faker.OpenQuestion(Faker.Lorem.Sentence(), Faker.Lorem.Sentence(), Faker.Lorem.Sentence());
                Validator = new OpenQuestionValidator(Question);
            }

            [Fact]
            public async Task AllAnswersAreCorrect()
            {
                foreach (var answer in Question.CorrectAnswers)
                {
                    var result = await Validator.Validate(answer);

                    Check.That(result.IsCorrect).IsTrue();
                }
            }
            
            [Fact]
            public async Task OtherAnswerIsIncorrect()
            {
                var result = await Validator.Validate(Faker.Lorem.Sentence());

                Check.That(result.IsCorrect).IsFalse();
            }
        }
    }
}