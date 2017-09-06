using System;
using System.Collections.Generic;
using LanguageExt;
using Rehearsal.Data.StructureMap;
using Rehearsal.Messages;

namespace Rehearsal.Data
{
    public class InMemoryStore<TEntity>
        where TEntity: class
    {
        public InMemoryStore()
        {
            Entities = new Dictionary<Guid, TEntity>();
        }

        protected IDictionary<Guid, TEntity> Entities { get; }

        public IEnumerable<TEntity> All => Entities.Values;

        public void Save(Guid id, TEntity entity)
        {
            Entities[id] = entity;
        }

        public Option<TEntity> GetById(Guid id) => Entities.TryGetValue(id);

        public void Remove(Guid id)
        {
            Entities.Remove(id);
        }
    }
}