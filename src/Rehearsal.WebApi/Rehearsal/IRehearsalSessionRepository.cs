using System;
using LanguageExt;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.WebApi.Rehearsal
{
    public interface IRehearsalSessionRepository
    {
        IRehearsalFactory New();
        Option<RehearsalSessionModel> GetById(Guid rehearsalId);
        Option<IRehearsalSession> GetSession(Guid rehearsalId);
    }
}