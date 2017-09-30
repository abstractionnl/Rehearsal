using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using Newtonsoft.Json;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;

namespace Rehearsal.Data.Infrastructure.StructureMap
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
                
                _.Convention<EventSerializerConvention>();
            });
        }

        private class EventSerializerConvention : IRegistrationConvention
        {
            public void ScanTypes(TypeSet types, Registry registry)
            {
                var eventTypes = types.FindTypes(TypeClassification.Concretes | TypeClassification.Closed)
                    .Where(type => typeof(IEvent).IsAssignableFrom(type))
                    .ToArray();

                registry.For<EventSerializer>().Use<EventSerializer>()
                    .Ctor<Type[]>().Is(eventTypes);
            }
        }
    }
}