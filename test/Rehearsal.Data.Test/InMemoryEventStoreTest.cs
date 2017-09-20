using System.Linq;
using Rehearsal.Data.Test.Mocks;

namespace Rehearsal.Data.Test
{
    public class InMemoryEventStoreTest : BaseEventStoreTest<InMemoryEventStore>
    {
        public InMemoryEventStoreTest()
        {
            EventStore = new InMemoryEventStore(EventPublisher);
        }
    }
}