using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.WebApi.Rehearsal
{
    public interface IRehearsalSessionRepository
    {
        IRehearsalFactory New();
    }
}