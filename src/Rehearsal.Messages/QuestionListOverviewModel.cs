using System;

namespace Rehearsal.Messages
{
    public class QuestionListOverviewModel
    {
        public QuestionListOverviewModel()
        {
        }
        
        public QuestionListOverviewModel(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string QuestionTitle { get; set; }
        public string AnswerTitle { get; set; }
        public int QuestionsCount { get; set; }
        public bool IsDeleted { get; set; }
    }
}