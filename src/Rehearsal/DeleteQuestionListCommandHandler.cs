using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Rehearsal.Messages;

namespace Rehearsal
{
    public class DeleteQuestionListCommandHandler : ICommandHandler<DeleteQuestionListCommand>
    {
        private readonly ISession _session;

        public DeleteQuestionListCommandHandler(ISession session)
        {
            _session = session;
        }
        
        public async Task Handle(DeleteQuestionListCommand message)
        {
            var questionList = await _session.Get<QuestionList>(message.Id);
            questionList.Delete();
            await _session.Commit();
        }
    }
}