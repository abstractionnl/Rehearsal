using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class RepeatRehearsalCommandHandler : ICommandHandler<RepeatRehearsalCommand>
    {
        private readonly ISession _session;

        public RepeatRehearsalCommandHandler(ISession session)
        {
            _session = session;
        }
        
        public async Task Handle(RepeatRehearsalCommand message)
        {
            var rehearsal = await _session.Get<Rehearsal>(message.RehearsalId);

            var newRehearsal = Rehearsal.CreateFrom(message.Id, rehearsal);

            await _session.Add(newRehearsal);
            await _session.Commit();
        }
    }
}