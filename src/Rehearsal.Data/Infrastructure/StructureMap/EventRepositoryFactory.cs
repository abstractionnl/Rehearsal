using System;
using CQRSlite.Events;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Rehearsal.Data.Infrastructure.StructureMap
{
    public class EventRepositoryFactory
    {
        public EventRepositoryFactory(IOptions<DatabaseOptions> options, Func<IEventPublisher> eventPublisher, Func<EventSerializer> eventSerializer)
        {
            Options = options.Value;
            EventPublisher = eventPublisher;
            EventSerializer = eventSerializer;
        }

        private DatabaseOptions Options { get; }
        private Func<IEventPublisher> EventPublisher { get; }
        private Func<EventSerializer> EventSerializer { get; }

        public IEventRepository Create()
        {
            switch (Options.EventStoreType)
            {
                case EventStoreType.InMemory:
                    return new InMemoryEventStore(
                        EventPublisher()
                    );
                case EventStoreType.Sqlite:
                    var store = new SqliteEventStore(
                        CreateSqlConnection(Options.ConnectionString), 
                        EventSerializer(), 
                        EventPublisher()
                    );

                    store.ProvisionTable().Wait(); // TODO: Move to initializer?
                    
                    return store;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
            
        public SqliteConnection CreateSqlConnection(string connectionString)
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}