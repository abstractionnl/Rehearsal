using System;
using System.Collections.Generic;
using System.Linq;
using CQRSlite.Domain;
using LanguageExt;
using Rehearsal.Messages;

namespace Rehearsal
{
    public class QuestionList : AggregateRoot
    {
        public string Title { get; private set; }
        public string QuestionTitle { get; private set; }
        public string AnswerTitle { get; private set; }

        public IList<ListItem> Questions { get; private set; }
        
        public bool IsDeleted { get; set; }

        [Obsolete("For CQRS")]
        private QuestionList() {}
        
        public QuestionList(Guid id, QuestionListProperties properties)
        {
            Id = id;
            
            ApplyChange(new QuestionListCreatedEvent()
            {
                Id = Id,
                QuestionList = properties
            });
        }

        public void Update(QuestionListProperties properties)
        {
            ApplyChange(new QuestionListUpdatedEvent()
            {
                Id = Id,
                QuestionList = properties
            });
        }

        public void Delete()
        {
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
            
            Questions = new List<ListItem>(properties.Questions.Select(x => ListItem.Create(x.Question, x.Answer)));
        }

        private QuestionListProperties ToProperties() => new QuestionListProperties
        {
            Title = Title,
            QuestionTitle = QuestionTitle,
            AnswerTitle = AnswerTitle,
            Questions = Questions.Select(q => new QuestionListProperties.Item
            {
                Question = q.Question,
                Answer = q.Answer
            }).ToList()
        };
        
        public class ListItem : Record<ListItem>
        {
            public string Question { get; internal set; }
            public string Answer { get; internal set; }

            public static ListItem Create(string question, string answer) => new ListItem
            {
                Question = question,
                Answer = answer
            };
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
