using System.Linq;
using Rehearsal.Data.Test.Mocks;

namespace Rehearsal.Data.Test
{
    public class InMemoryEventRepositoryTest : BaseEventRepositoryTest<InMemoryEventStore>
    {
        public InMemoryEventRepositoryTest()
        {
            EventStore = new InMemoryEventStore(EventPublisher);
        }
    }
}