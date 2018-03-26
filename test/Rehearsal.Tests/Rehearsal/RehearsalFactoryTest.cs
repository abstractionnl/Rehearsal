using System.Threading.Tasks;
using CQRSlite.Commands;
using NFluent;
using Rehearsal.Messages.Rehearsal;
using Rehearsal.Rehearsal;
using Rehearsal.Tests.Mocks;
using Rehearsal.Xunit;
using Xunit;

namespace Rehearsal.Tests.Rehearsal
{
    public class RehearsalFactoryTest : TestBase
    {
        public RehearsalFactoryTest()
        {
        }
        
        [Fact]
        public async Task CanConstructEmptyRehearsalSession()
        {
            var cmd = await new RehearsalFactory()
                .Create();

            Check.That(cmd.Questions).IsEmpty();
        }

        [Fact]
        public async Task CanAddOneQuestionListToSession()
        {
            var questionList = Faker.QuestionList();
            
            var cmd = await new RehearsalFactory()
                .AddQuestionList(questionList)
                .Create();
            
            Check.That(cmd.Questions).HasSize(questionList.Questions.Count);
        }

        [Fact]
        public async Task WhenQuestionHasMultipleCorrectAnswersQuestionIsGrouped()
        {
            var q = Faker.Lorem.Word();
            
            var questionList = Faker.QuestionList(Faker.Question(q), Faker.Question(q));
            
            var cmd = await new RehearsalFactory()
                .AddQuestionList(questionList)
                .Create();

            Check.That(cmd.Questions)
                .HasOneElementOnly()
                .And.HasElementThatMatches(s => s.Question == q);
        }
        
        [Fact]
        public async Task WhenQuestionHasMultipleCorrectAnswersMultipleAnswersAreCorrect()
        {
            var q = Faker.Lorem.Word();
            var answers = Faker.Lorem.Words(2);
            
            var questionList = Faker.QuestionList(Faker.Question(q, answers[0]), Faker.Question(q, answers[1]));
            
            var cmd = await new RehearsalFactory()
                .AddQuestionList(questionList)
                .Create();

            Check.That(cmd.Questions)
                .HasElementThatMatches(s => s.Question == q)
                .Which.Selecting(p => ((OpenRehearsalQuestionModel)p).CorrectAnswers)
                .ContainsExactly(answers);
        }
        
        [Fact]
        public async Task WithMultipleSameAnswersOneAnswerIsCorrect()
        {
            var q = Faker.Lorem.Word();
            var a = Faker.Lorem.Word();
            
            var questionList = Faker.QuestionList(Faker.Question(q, a), Faker.Question(q, a));
            
            var cmd = await new RehearsalFactory()
                .AddQuestionList(questionList)
                .Create();

            Check.That(cmd.Questions)
                .HasElementThatMatches(s => s.Question == q)
                .Which.Selecting(p => ((OpenRehearsalQuestionModel) p).CorrectAnswers)
                .ContainsExactly(a);
        }
    }
}