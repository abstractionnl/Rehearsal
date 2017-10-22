using System;
using System.Threading.Tasks;

namespace Rehearsal.Messages.Rehearsal
{
    public interface IRehearsalSession
    {
        Task<AnswerResultModel> GiveAnswer(Guid questionId, string answer);
    }
}