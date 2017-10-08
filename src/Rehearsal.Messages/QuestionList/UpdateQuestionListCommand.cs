using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.QuestionList
{
    public class UpdateQuestionListCommand : BaseCommand
    {
        public QuestionListProperties QuestionList { get; set; }
    }
}