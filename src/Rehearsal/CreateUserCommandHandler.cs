using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Rehearsal.Messages;

namespace Rehearsal
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly ISession _session;

        public CreateUserCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateUserCommand message)
        {
            var user = new User(message.Id, message.Username);
            await _session.Add(user);
            await _session.Commit();
        }
    }
}