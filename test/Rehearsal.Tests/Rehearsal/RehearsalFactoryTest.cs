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
        private MockedCommandSender CommandSender { get; }
        
        public RehearsalFactoryTest()
        {
            CommandSender = new MockedCommandSender();
        }
        
        [Fact]
        public async Task CanConstructEmptyRehearsalSession()
        {
            var sessionId = await new RehearsalFactory(CommandSender)
                .Create();

            Check.That(CommandSender.SentCommands)
                .ContainsInstanceOf<ICommand, StartRehearsalCommand>()
                .Which.PerformAssertions(
                    cmd => Check.That(cmd.Questions).IsEmpty());
        }

        [Fact]
        public async Task CanAddOneQuestionListToSession()
        {
            var questionList = Faker.QuestionList();
            
            var sessionId = await new RehearsalFactory(CommandSender)
                .AddQuestionList(questionList)
                .Create();
            
            Check.That(CommandSender.SentCommands)
                .ContainsInstanceOf<ICommand, StartRehearsalCommand>()
                .Which.PerformAssertions(
                    cmd => Check.That(cmd.Questions).ContainsExactly(questionList.Questions));
        }
    }
}