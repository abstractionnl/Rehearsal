using System;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace Rehearsal.Data.Test
{
    public class SqliteEventStoreTest : BaseEventStoreTest<SqliteEventStore>, IDisposable
    {
        private SqliteConnection Connection { get; }
        
        public SqliteEventStoreTest()
        {
            Connection = new SqliteConnection("Datasource=:memory:;");
            Connection.Open();
            EventStore = new SqliteEventStore(Connection, JsonSerializer.CreateDefault(), EventPublisher);
            EventStore.ProvisionTable().Wait();
        }

        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
        }
    }
}