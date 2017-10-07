using System;
using System.Threading.Tasks;

namespace Rehearsal.Messages
{
    public interface IRehearsalFactory
    {
        Task<Guid> Create();
        IRehearsalFactory AddQuestionList(QuestionListModel questionList);
    }
}