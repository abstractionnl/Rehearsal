using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CQRSlite.Events;
using Rehearsal.Data.Infrastructure;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Data.Rehearsal
{
    public class RehearsalSessionEventHandler : IEventHandler<RehearsalStartedEvent>, IEventHandler<QuestionAnsweredEvent>
    {
        public RehearsalSessionEventHandler(InMemoryStore<RehearsalSessionModel> store, IMapper mapper)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private InMemoryStore<RehearsalSessionModel> Store { get; }
        private IMapper Mapper { get; }

        public Task Handle(RehearsalStartedEvent message)
        {
            var session = Mapper.Map<RehearsalSessionModel>(message);
            Store.Save(message.Id, session);

            return Task.CompletedTask;
        }

        public Task Handle(QuestionAnsweredEvent message)
        {
            var session = Store.GetById(message.Id)
                .IfNone(() => throw new InvalidOperationException($"Rehearsal with id {message.Id} not found"));
            
            var question = session.Questions
                .Where(x => x.Id == message.QuestionId)
                .HeadOrNone()
                .IfNone(() => throw new InvalidOperationException($"Question with id {message.QuestionId} not found"));

            question.GivenAnswer = message.GivenAnswer;
            question.AnsweredCorrectly = message.IsCorrect;
            
            Store.Save(message.Id, session);
            
            return Task.CompletedTask;
        }
    }
}