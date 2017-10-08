using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.Authorization
{
    public class CreateUserCommand : BaseCommand
    {
        public string Username { get; set; }
    }
}