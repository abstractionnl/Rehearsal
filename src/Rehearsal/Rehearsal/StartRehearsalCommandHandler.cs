using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class StartRehearsalCommandHandler : ICommandHandler<StartRehearsalCommand>
    {
        private readonly ISession _session;

        public StartRehearsalCommandHandler(ISession session)
        {
            _session = session;
        }
        
        public async Task Handle(StartRehearsalCommand message)
        {
            var questionList = await _session.Get<QuestionList.QuestionList>(message.QuestionListId);
            
            var factory = new RehearsalFactory();

            var questions = factory.AddQuestionList(questionList)
                .SetQuestionType(message.QuestionType)
                .Create();
            
            var rehearsal = new Rehearsal(message.Id, questions);
            await _session.Add(rehearsal);
            await _session.Commit();
        }
    }
}