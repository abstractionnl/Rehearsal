using System;
using System.Threading.Tasks;
using AutoMapper;
using CQRSlite.Events;
using Rehearsal.Data.Infrastructure;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Data.Rehearsal
{
    public class RehearsalSessionEventHandler : IEventHandler<RehearsalStartedEvent>
    {
        public RehearsalSessionEventHandler(InMemoryStore<RehearsalSessionModel> store, IMapper mapper)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private InMemoryStore<RehearsalSessionModel> Store { get; }
        private IMapper Mapper { get; }

        public Task Handle(RehearsalStartedEvent message)
        {
            var session = Mapper.Map<RehearsalSessionModel>(message);
            Store.Save(message.Id, session);

            return Task.CompletedTask;
        }
    }
}