using System.Threading.Tasks;
using CQRSlite.Events;
using Rehearsal.Data.Infrastructure;
using Rehearsal.Messages;

namespace Rehearsal.Data
{
    public class UserEventHandler : IEventHandler<UserCreatedEvent>
    {
        public UserEventHandler(InMemoryStore<UserModel> userStore)
        {
            UserStore = userStore;
        }

        private InMemoryStore<UserModel> UserStore { get; }
        
        public Task Handle(UserCreatedEvent message)
        {
            UserStore.Save(message.Id, new UserModel() { Id = message.Id, UserName = message.Username });
            return Task.CompletedTask;
        }
    }
}