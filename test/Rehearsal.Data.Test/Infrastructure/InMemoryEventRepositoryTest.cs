using Rehearsal.Data.Infrastructure;

namespace Rehearsal.Data.Test.Infrastructure
{
    public class InMemoryEventRepositoryTest : BaseEventRepositoryTest<InMemoryEventStore>
    {
        public InMemoryEventRepositoryTest()
        {
            EventStore = new InMemoryEventStore(EventPublisher);
        }
    }
}