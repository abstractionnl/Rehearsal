using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace Rehearsal.Data
{
    public class SqliteEventStore : IEventStore
    {
        private IEventPublisher EventPublisher { get; }
        private SqliteConnection Connection { get; }
        private JsonSerializer Serializer { get; }

        const string CreateSyntax = @"CREATE TABLE IF NOT EXISTS events (
	        Id VARCHAR(36),
	        Version INT,
	        TimeStamp LONG,
	        Type VARCHAR(255),
	        Data TEXT)";

        private const string InsertSyntax = @"INSERT INTO events (Id, Version, TimeStamp, Type, Data) VALUES (@id, @version, @timestamp, @type, @data)";
        private const string SelectSyntax = @"SELECT Type, Data FROM events WHERE Id = @id AND Version > @fromVersion";
        
        public SqliteEventStore(SqliteConnection connection, JsonSerializer serializer, IEventPublisher eventPublisher)
        {
            Connection = connection;
            Serializer = serializer;
            EventPublisher = eventPublisher;
        }
        
        public async Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var tran = Connection.BeginTransaction()) {
                var command = Connection.CreateCommand();
                command.CommandText = InsertSyntax;

                var idParam = command.Parameters.Add("@id", SqliteType.Text);
                var verionParam = command.Parameters.Add("@version", SqliteType.Integer);
                var timestampParam = command.Parameters.Add("@timestamp", SqliteType.Text);
                var typeParam = command.Parameters.Add("@type", SqliteType.Text);
                var dataParam = command.Parameters.Add("@data", SqliteType.Text);
                
                foreach (var @event in events)
                {
                    idParam.Value = @event.Id;
                    verionParam.Value = @event.Version;
                    timestampParam.Value = @event.TimeStamp;
                    typeParam.Value = @event.GetType().AssemblyQualifiedName;
                    dataParam.Value = Serialize(@event);
                        
                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
                
                tran.Commit();

                foreach (var @event in events)
                {
                    await PublishEvent(@event, cancellationToken);
                }
            }
        }

        public async Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = new CancellationToken())
        {
            var command = Connection.CreateCommand();
            command.CommandText = SelectSyntax;

            var idParam = command.Parameters.Add("@id", SqliteType.Text);
            var fromVersionParam = command.Parameters.Add("@fromVersion", SqliteType.Integer);

            idParam.Value = aggregateId;
            fromVersionParam.Value = fromVersion;

            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
            {
                var result = new List<IEvent>();
                
                while (await reader.ReadAsync(cancellationToken))
                {
                    var typeAsString = reader.GetString(0);
                    var data = reader.GetString(1);

                    var type = Type.GetType(typeAsString, true);
                    result.Add(Deserialize(data, type));
                }

                return result;
            }
        }

        public async Task ProvisionTable()
        {
            var command = Connection.CreateCommand();
            command.CommandText = CreateSyntax;
            await command.ExecuteNonQueryAsync();
        }
        
        private Task PublishEvent(IEvent @event, CancellationToken cancellationToken)
        {
            return (Task)typeof(IEventPublisher).GetMethod(nameof(IEventPublisher.Publish))
                .MakeGenericMethod(@event.GetType())
                .Invoke(EventPublisher, new object[] { @event, cancellationToken });
        }

        private string Serialize(IEvent @event)
        {
            var sb = new StringBuilder(256);
            var sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            using (var jsonWriter = new JsonTextWriter(sw))
            {
                Serializer.Serialize(jsonWriter, @event);
            }

            return sw.ToString();
        }

        private IEvent Deserialize(string json, Type type)
        {
            using (var reader = new JsonTextReader(new StringReader(json)))
            {
                return (IEvent)Serializer.Deserialize(reader, type);
            }
        }
    }
}