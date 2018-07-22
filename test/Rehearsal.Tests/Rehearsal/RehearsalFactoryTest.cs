using System.Linq;
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
            var questions = new RehearsalFactory()
                .Create();

            Check.That(questions).IsEmpty();
        }

        [Fact]
        public void CanAddOneQuestionListToSession()
        {
            var questionList = Faker.QuestionList();
            
            var questions = new RehearsalFactory()
                .AddQuestionList(questionList)
                .Create();
            
            Check.That(questions).HasSize(questionList.Questions.Count);
        }

        [Fact]
        public void WhenQuestionHasMultipleCorrectAnswersQuestionIsGrouped()
        {
            var q = Faker.Lorem.Word();
            
            var questionList = Faker.QuestionList(Faker.Question(q), Faker.Question(q));
            
            var questions = new RehearsalFactory()
                .AddQuestionList(questionList)
                .Create();

            Check.That(questions)
                .HasOneElementOnly()
                .And.HasElementThatMatches(s => s.Question == q);
        }
        
        [Fact]
        public void WhenQuestionHasMultipleCorrectAnswersMultipleAnswersAreCorrect()
        {
            var q = Faker.Lorem.Word();
            var answers = Faker.Lorem.Words(2);
            
            var questionList = Faker.QuestionList(Faker.Question(q, answers[0]), Faker.Question(q, answers[1]));
            
            var questions = new RehearsalFactory()
                .AddQuestionList(questionList)
                .Create();

            Check.That(questions)
                .HasElementThatMatches(s => s.Question == q)
                .Which.IsInstanceOf<OpenRehearsalQuestionModel>()
                .And.Selecting(p => ((OpenRehearsalQuestionModel)p).CorrectAnswers)
                .ContainsExactly(answers);
        }
        
        [Fact]
        public void WithMultipleSameAnswersOneAnswerIsCorrect()
        {
            var q = Faker.Lorem.Word();
            var a = Faker.Lorem.Word();
            
            var questionList = Faker.QuestionList(Faker.Question(q, a), Faker.Question(q, a));
            
            var questions = new RehearsalFactory()
                .AddQuestionList(questionList)
                .Create();

            Check.That(questions)
                .HasElementThatMatches(s => s.Question == q)
                .Which.IsInstanceOf<OpenRehearsalQuestionModel>()
                .And.Selecting(p => ((OpenRehearsalQuestionModel) p).CorrectAnswers)
                .ContainsExactly(a);
        }

        [Fact]
        public void CanCreateMultipleChoiceRehearsal()
        {
            var questionList = Faker.QuestionList(3);
            
            var questions = new RehearsalFactory()
                .AddQuestionList(questionList)
                .UseMultipleChoiceQuestions(3)
                .Create();

            var q = questionList.Questions.First();
            
            Check.That(questions)
                .HasElementThatMatches(s => s.Question == q.Question)
                .Which.IsInstanceOf<MultipleChoiceQuestionModel>()
                .And.Selecting(x => (MultipleChoiceQuestionModel)x)
                .Satisfies(
                    qm => qm.Selecting(x => x.AvailableAnswers).Contains(questionList.Questions.Select(x => x.Answer)),
                    qm => qm.Selecting(x => x.AvailableAnswers[x.CorrectAnswer]).IsEqualTo(q.Answer)
                );
        }

        [Fact]
        public void MultipleChoiceCanHandleTooLittleOtherAnswers()
        {
            var questionList = Faker.QuestionList(3);
            
            var questions = new RehearsalFactory()
                .AddQuestionList(questionList)
                .UseMultipleChoiceQuestions(10)
                .Create();
            
            var q = questionList.Questions.First();
            
            Check.That(questions)
                .HasElementThatMatches(s => s.Question == q.Question)
                .Which.IsInstanceOf<RehearsalQuestionModel, MultipleChoiceQuestionModel>()
                .And.Satisfies(
                    qm => qm.Selecting(x => x.AvailableAnswers).Contains(questionList.Questions.Select(x => x.Answer)),
                    qm => qm.Selecting(x => x.AvailableAnswers[x.CorrectAnswer]).IsEqualTo(q.Answer)
                );
        }

        [Fact]
        public void MultipleChoiceDoesNotShowSameAnswerTwice()
        {
            var a = Faker.Lorem.Words(3);
            
            var questionList = Faker.QuestionList(
                Faker.Question(Faker.Lorem.Word(), a[0]), 
                Faker.Question(Faker.Lorem.Word(), a[0]),
                Faker.Question(Faker.Lorem.Word(), a[1]),
                Faker.Question(Faker.Lorem.Word(), a[2]));
            
            var questions = new RehearsalFactory()
                .AddQuestionList(questionList)
                .UseMultipleChoiceQuestions(3)
                .Create();
            
            var q = questionList.Questions.First();

            Check.That(questions)
                .HasElementThatMatches(s => s.Question == q.Question)
                .Which.IsInstanceOf<RehearsalQuestionModel, MultipleChoiceQuestionModel>()
                .And.Satisfies(
                    qm => qm.Selecting(x => x.AvailableAnswers).HasSize(3)
                        .And.Contains(questionList.Questions.Select(x => x.Answer).Distinct())
                );
        }
    }
}