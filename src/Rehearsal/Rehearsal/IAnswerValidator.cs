using System.Threading.Tasks;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public interface IAnswerValidator
    {
        Task<AnswerResultModel> Validate(string answer);
    }
}