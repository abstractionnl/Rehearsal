using System;
using CQRSlite.Events;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Rehearsal.Data.Infrastructure.StructureMap
{
    public class EventRepositoryFactory
    {
        public EventRepositoryFactory(IOptions<DatabaseOptions> options, IEventPublisher eventPublisher, JsonSerializer jsonSerializer)
        {
            Options = options.Value;
            EventPublisher = eventPublisher;
            JsonSerializer = jsonSerializer;
        }

        public DatabaseOptions Options { get; }
        public IEventPublisher EventPublisher { get; }
        public JsonSerializer JsonSerializer { get; }

        public IEventRepository Create()
        {
            switch (Options.EventStoreType)
            {
                case EventStoreType.InMemory:
                    return new InMemoryEventStore(
                        EventPublisher
                    );
                case EventStoreType.Sqlite:
                    var store = new SqliteEventStore(
                        CreateSqlConnection(Options.ConnectionString), 
                        JsonSerializer, 
                        EventPublisher
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