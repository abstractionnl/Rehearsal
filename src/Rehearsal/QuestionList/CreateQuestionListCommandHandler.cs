using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Rehearsal.Messages;
using Rehearsal.Messages.QuestionList;

namespace Rehearsal.QuestionList
{
    public class CreateQuestionListCommandHandler : ICommandHandler<CreateQuestionListCommand>
    {
        private readonly ISession _session;

        public CreateQuestionListCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateQuestionListCommand message)
        {
            var questionList = new QuestionList(message.Id, message.QuestionList);
            await _session.Add(questionList);
            await _session.Commit();
        }
    }
}