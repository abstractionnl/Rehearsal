using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages
{
    public class UpdateQuestionListCommand : BaseCommand
    {
        public QuestionListProperties QuestionList { get; set; }
    }
}