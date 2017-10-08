using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Rehearsal.Messages;
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
            var rehearsal = new Rehearsal(message.Id, message.Questions);
            await _session.Add(rehearsal);
            await _session.Commit();
        }
    }
}