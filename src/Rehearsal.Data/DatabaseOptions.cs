﻿namespace Rehearsal.Data
{
    public class DatabaseOptions
    {
        public EventStoreType EventStoreType { get; set; } = EventStoreType.InMemory;
        public string ConnectionString { get; set; }
    }
    
    public enum EventStoreType
    {
        InMemory, Sqlite
    }
}