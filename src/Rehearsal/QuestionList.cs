using System;
using System.Collections.Generic;
using Rehearsal.Infrastructure;

namespace Rehearsal
{
    public class QuestionList
    {
        public Guid Id { get; }
        public string Title { get; }
        public string QuestionTitle { get; }
        public string AnswerTitle { get; }

        public IList<ListItem> Questions { get; }

        public QuestionList(string title, string questionTitle, string answerTitle)
        {
            Id = Guid.NewGuid();
            
            Title = title;
            QuestionTitle = questionTitle;
            AnswerTitle = answerTitle;
            
            Questions = new List<ListItem>();
        }

        public class ListItem : ValueObject<ListItem>
        {
            public string Question { get; internal set; }
            public string Answer { get; internal set; }

            public static ListItem Create(string question, string answer) => new ListItem
            {
                Question = question,
                Answer = answer
            };

            protected override IEnumerable<Func<ListItem, object>> GetCompareProperties()
            {
                yield return x => x.Question;
                yield return x => x.Answer;
            }
        }
    }
}
