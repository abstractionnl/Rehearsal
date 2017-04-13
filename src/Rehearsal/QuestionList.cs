using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;

namespace Rehearsal
{
    public class QuestionList : Entity<QuestionListId, QuestionList.QuestionListState>
    {
        public string Title => State.Title;
        public string QuestionTitle => State.QuestionTitle;
        public string AnswerTitle => State.AnswerTitle;

        public IList<ListItem> Questions => State.Questions;

        public QuestionList(string title, string questionTitle, string answerTitle) : base(
            new QuestionList.QuestionListState
            {
                Title = title,
                AnswerTitle = answerTitle,
                QuestionTitle = questionTitle,
                Questions = new List<ListItem>()
            })
        {
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

        public class QuestionListState : EntityState<QuestionListId>
        {
            public string Title { get; set; }
            public string QuestionTitle { get; set; }
            public string AnswerTitle { get; set; }

            public IList<ListItem> Questions { get; set; }
        }
    }

    public class QuestionListId : Identity<Guid>
    {
        

        public QuestionListId(Guid value) : base(value)
        {
        }
    }
}
