using System;
using CQRSlite.Domain;
using Rehearsal.Messages;
using Rehearsal.Messages.Authorization;

namespace Rehearsal.Authorization
{
    public class User : AggregateRoot
    {
        public string Username { get; private set; }
        
        public User(Guid id, string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(username));
            
            Id = id;
            
            ApplyChange(new UserCreatedEvent()
            {
                Username = username
            });
        }
        
        protected void Apply(UserCreatedEvent @event)
        {
            Username = @event.Username;
        }
    }
}