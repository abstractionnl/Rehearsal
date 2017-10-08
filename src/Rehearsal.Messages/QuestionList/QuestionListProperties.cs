using System.Collections.Generic;

namespace Rehearsal.Messages.QuestionList
{
    public class QuestionListProperties
    {
        public string Title { get; set; }
        public string QuestionTitle { get; set; }
        public string AnswerTitle { get; set; }
        public ICollection<QuestionModel> Questions { get; set; }
    }
}