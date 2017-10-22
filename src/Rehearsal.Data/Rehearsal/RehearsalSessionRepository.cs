using System;
using CQRSlite.Commands;
using LanguageExt;
using Rehearsal.Data.Infrastructure;
using Rehearsal.Messages.Rehearsal;
using Rehearsal.Rehearsal;
using Rehearsal.WebApi.Rehearsal;

namespace Rehearsal.Data.Rehearsal
{
    public class RehearsalSessionRepository : IRehearsalSessionRepository
    {
        private InMemoryStore<RehearsalSessionModel> SessionStore { get; }
        
        public RehearsalSessionRepository(InMemoryStore<RehearsalSessionModel> sessionStore)
        {
            SessionStore = sessionStore;
        }
        
        public IRehearsalFactory New()
        {
            return new RehearsalFactory();
        }

        public Option<RehearsalSessionModel> GetById(Guid rehearsalId) => 
            SessionStore.GetById(rehearsalId);

        public Option<IRehearsalSession> GetSession(Guid rehearsalId) => 
            SessionStore.GetById(rehearsalId)
                .Map<IRehearsalSession>(session => new RehearsalSession(session.Questions, new AnswerValidatorFactory()));
    }
}