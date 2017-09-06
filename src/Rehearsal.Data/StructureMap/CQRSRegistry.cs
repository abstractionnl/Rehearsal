using CQRSlite.Bus;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using StructureMap;

namespace Rehearsal.Data.StructureMap
{
    public class CqrsRegistry : Registry
    {
        public CqrsRegistry()
        {
            For<InProcessBus>().Singleton().Use<InProcessBus>();
            For<ICommandSender>().Use(y => y.GetInstance<InProcessBus>());
            For<IEventPublisher>().Use(y => y.GetInstance<InProcessBus>());
            For<ISession>().Use<Session>();
            For<IEventStore>().Singleton().Use<InMemoryEventStore>();
            For<IRepository>().Use(y => new Repository(y.GetInstance<IEventStore>()));
            
            Scan(_ =>
            {
                _.AssembliesFromApplicationBaseDirectory();
                _.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                _.ConnectImplementationsToTypesClosing(typeof(IEventHandler<>));
            });
        }
    }
}