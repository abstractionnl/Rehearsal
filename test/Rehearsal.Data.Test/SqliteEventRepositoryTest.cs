using System;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace Rehearsal.Data.Test
{
    public class SqliteEventRepositoryTest : BaseEventRepositoryTest<SqliteEventStore>, IDisposable
    {
        private SqliteConnection Connection { get; }
        
        public SqliteEventRepositoryTest()
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