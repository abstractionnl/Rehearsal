using System;
using System.Threading.Tasks;
using Rehearsal.Messages.QuestionList;

namespace Rehearsal.Messages.Rehearsal
{
    public interface IRehearsalFactory
    {
        Task<Guid> Create();
        IRehearsalFactory AddQuestionList(QuestionListModel questionList);
    }
}