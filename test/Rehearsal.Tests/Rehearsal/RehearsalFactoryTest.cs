using System.Threading.Tasks;
using CQRSlite.Commands;
using NFluent;
using Rehearsal.Messages.Rehearsal;
using Rehearsal.Rehearsal;
using Rehearsal.Tests.Mocks;
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
    }
}