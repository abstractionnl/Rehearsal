using System;
using System.Collections.Generic;
using System.Linq;
using CQRSlite.Domain;
using Rehearsal.Messages;

namespace Rehearsal
{
    public class QuestionList : AggregateRoot
    {
        public string Title { get; private set; }
        public string QuestionTitle { get; private set; }
        public string AnswerTitle { get; private set; }

        public IList<QuestionRecord> Questions { get; private set; }
        
        public bool IsDeleted { get; set; }

        [Obsolete("For CQRS")]
        private QuestionList() {}
        
        public QuestionList(Guid id, QuestionListProperties properties)
        {
            if (properties == null) throw new ArgumentNullException(nameof(properties));
            
            Id = id;
            
            ApplyChange(new QuestionListCreatedEvent()
            {
                Id = Id,
                QuestionList = properties
            });
        }

        public void Update(QuestionListProperties properties)
        {
            if (properties == null) throw new ArgumentNullException(nameof(properties));
            
            ApplyChange(new QuestionListUpdatedEvent()
            {
                Id = Id,
                QuestionList = properties
            });
        }

        public void Delete()
        {
            if (IsDeleted)
                throw new InvalidOperationException("Cannot delete questionlist twice");
            
            ApplyChange(new QuestionListDeletedEvent()
            {
                Id = Id
            });
        }

        private void FromProperties(QuestionListProperties properties)
        {
            Title = properties.Title;
            QuestionTitle = properties.QuestionTitle;
            AnswerTitle = properties.AnswerTitle;
            
            Questions = new List<QuestionRecord>(properties.Questions.Select(x => QuestionRecord.Create(x.Question, x.Answer)));
        }
        
        protected void Apply(QuestionListCreatedEvent @event)
        {
            FromProperties(@event.QuestionList);
        }
        
        protected void Apply(QuestionListUpdatedEvent @event)
        {
            FromProperties(@event.QuestionList);
        }
        
        protected void Apply(QuestionListDeletedEvent @event)
        {
            IsDeleted = true;
        }
    }
}
