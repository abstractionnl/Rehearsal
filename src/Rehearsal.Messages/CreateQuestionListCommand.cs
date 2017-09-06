using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages
{
    public class CreateQuestionListCommand : BaseCommand
    {
        public QuestionListProperties QuestionList { get; set; }
    }
}