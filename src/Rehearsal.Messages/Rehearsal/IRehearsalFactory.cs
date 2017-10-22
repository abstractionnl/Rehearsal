using System;
using System.Threading.Tasks;
using Rehearsal.Messages.QuestionList;

namespace Rehearsal.Messages.Rehearsal
{
    public interface IRehearsalFactory
    {
        Task<StartRehearsalCommand> Create();
        IRehearsalFactory AddQuestionList(QuestionListModel questionList);
    }
}