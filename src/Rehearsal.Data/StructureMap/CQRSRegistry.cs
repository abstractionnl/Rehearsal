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
            ForSingletonOf<InProcessBus>().Use<InProcessBus>();
            For<ICommandSender>().Use(y => y.GetInstance<InProcessBus>());
            For<IEventPublisher>().Use(y => y.GetInstance<InProcessBus>());
            For<ISession>().Use<Session>();
            ForConcreteType<EventRepositoryFactory>();
            For<IEventStore>().Use(y => y.GetInstance<IEventRepository>());
            ForSingletonOf<IEventRepository>().Use(y => y.GetInstance<EventRepositoryFactory>().Create());
            
            For<IRepository>().Use<Repository>();
            
            Scan(_ =>
            {
                _.AssembliesFromApplicationBaseDirectory();
                _.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                _.ConnectImplementationsToTypesClosing(typeof(IEventHandler<>));
            });
        }
    }
}