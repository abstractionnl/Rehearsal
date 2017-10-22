using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class AnswerRehearsalQuestionCommandHandler : ICommandHandler<AnswerRehearsalQuestionCommand>
    {
        private readonly ISession _session;

        public AnswerRehearsalQuestionCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task Handle(AnswerRehearsalQuestionCommand message)
        {
            var rehearsal = await _session.Get<Rehearsal>(message.Id);
            rehearsal.GiveAnswer(message.QuestionId, message.Result);

            await _session.Commit();
        }
    }
}