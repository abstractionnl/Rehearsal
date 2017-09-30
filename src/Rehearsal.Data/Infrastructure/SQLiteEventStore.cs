using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;
using Microsoft.Data.Sqlite;

namespace Rehearsal.Data.Infrastructure
{
    public class SqliteEventStore : IEventRepository
    {
        private IEventPublisher EventPublisher { get; }
        private SqliteConnection Connection { get; }
        private EventSerializer Serializer { get; }

        const string CreateSyntax = @"CREATE TABLE IF NOT EXISTS events (
	        Id VARCHAR(36),
	        Version INT,
	        TimeStamp LONG,
	        Type VARCHAR(255),
	        Data TEXT)";

        private const string InsertSyntax = @"INSERT INTO events (Id, Version, TimeStamp, Type, Data) VALUES (@id, @version, @timestamp, @type, @data)";
        private const string SelectSyntax = @"SELECT Type, Data FROM events WHERE Id = @id AND Version > @fromVersion";
        private const string GetAllEventsSyntax = @"SELECT Type, Data FROM events ORDER BY TimeStamp";
        
        public SqliteEventStore(SqliteConnection connection, EventSerializer serializer, IEventPublisher eventPublisher)
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
                    (typeParam.Value, dataParam.Value) = Serializer.Serialize(@event);
                        
                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
                
                tran.Commit();

                foreach (var @event in events)
                {
                    await EventPublisher.PublishEvent(@event, cancellationToken);
                }
            }
        }

        public Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = new CancellationToken())
        {
            var command = Connection.CreateCommand();
            command.CommandText = SelectSyntax;

            var idParam = command.Parameters.Add("@id", SqliteType.Text);
            var fromVersionParam = command.Parameters.Add("@fromVersion", SqliteType.Integer);

            idParam.Value = aggregateId;
            fromVersionParam.Value = fromVersion;

            return ReadFromCommand(command, cancellationToken);
        }

        public IObservable<IEvent> GetEventStream()
        {
            return Observable.Create<IEvent>(async (observer, cancelationToken) =>
            {
                var command = Connection.CreateCommand();
                command.CommandText = GetAllEventsSyntax;
                
                await StreamCommandResult(command, observer, cancelationToken);
            });
        }

        private async Task StreamCommandResult(SqliteCommand command, IObserver<IEvent> observer, CancellationToken cancel)
        {
            using (var reader = await command.ExecuteReaderAsync(CancellationToken.None))
            {
                while (!cancel.IsCancellationRequested && await reader.ReadAsync(CancellationToken.None))
                {
                    var typeAsString = reader.GetString(0);
                    var data = reader.GetString(1);
                    
                    observer.OnNext(Serializer.Deserialize(typeAsString, data));
                }

                observer.OnCompleted();
            }
        }

        private async Task<IEnumerable<IEvent>> ReadFromCommand(SqliteCommand command, CancellationToken cancellationToken)
        {
            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
            {
                var result = new List<IEvent>();
                
                while (await reader.ReadAsync(cancellationToken))
                {
                    var typeAsString = reader.GetString(0);
                    var data = reader.GetString(1);

                    result.Add(Serializer.Deserialize(typeAsString, data));
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
    }
}