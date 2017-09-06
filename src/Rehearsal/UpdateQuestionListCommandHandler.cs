using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Rehearsal.Messages;

namespace Rehearsal
{
    public class UpdateQuestionListCommandHandler : ICommandHandler<UpdateQuestionListCommand>
    {
        private readonly ISession _session;

        public UpdateQuestionListCommandHandler(ISession session)
        {
            _session = session;
        }
        
        public async Task Handle(UpdateQuestionListCommand message)
        {
            var questionList = await _session.Get<QuestionList>(message.Id);
            questionList.Update(message.QuestionList);
            await _session.Commit();
        }
    }
}