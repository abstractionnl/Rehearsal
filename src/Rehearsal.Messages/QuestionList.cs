using System;

namespace Rehearsal.Messages
{
    public class QuestionList
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string QuestionTitle { get; set; }
        public string AnswerTitle { get; set; }
        public Item[] Questions { get; set; }

        public class Item
        {
            public string Question { get; set; }
            public string Answer { get; set; }
        }
    }
}