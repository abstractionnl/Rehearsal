using System;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using Rehearsal.Data.Infrastructure;
using Rehearsal.Data.Test.Mocks;

namespace Rehearsal.Data.Test.Infrastructure
{
    public class SqliteEventRepositoryTest : BaseEventRepositoryTest<SqliteEventStore>, IDisposable
    {
        private SqliteConnection Connection { get; }
        
        public SqliteEventRepositoryTest()
        {
            Connection = new SqliteConnection("Datasource=:memory:;");
            Connection.Open();
            var eventSerializer = new MockedEventSerializer();
            
            EventStore = new SqliteEventStore(Connection, eventSerializer, EventPublisher);
            EventStore.ProvisionTable().Wait();
        }

        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
        }
    }
}