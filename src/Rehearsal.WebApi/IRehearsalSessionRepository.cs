using Rehearsal.Messages;

namespace Rehearsal.WebApi
{
    public interface IRehearsalSessionRepository
    {
        IRehearsalFactory New();
    }
}