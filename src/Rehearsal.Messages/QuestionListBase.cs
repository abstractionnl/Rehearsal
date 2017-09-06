using System;
using System.Collections.Generic;

namespace Rehearsal.Messages
{
    public class QuestionListProperties
    {
        public string Title { get; set; }
        public string QuestionTitle { get; set; }
        public string AnswerTitle { get; set; }
        public ICollection<Item> Questions { get; set; }

        public class Item
        {
            public string Question { get; set; }
            public string Answer { get; set; }
        }
    }
}