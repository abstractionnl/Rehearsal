using System.Threading.Tasks;
using AutoMapper;
using CQRSlite.Events;
using Rehearsal.Messages;
using Rehearsal.WebApi;

namespace Rehearsal.Data
{
    public class QuestionListEventHandler : 
        IEventHandler<QuestionListCreatedEvent>,
        IEventHandler<QuestionListUpdatedEvent>,
        IEventHandler<QuestionListDeletedEvent>
    {
        public QuestionListEventHandler(InMemoryStore<QuestionListModel> questionListStore, InMemoryStore<QuestionListOverviewModel> questionListOverviewStore, IMapper mapper)
        {
            QuestionListStore = questionListStore;
            QuestionListOverviewStore = questionListOverviewStore;
            Mapper = mapper;
        }

        public InMemoryStore<QuestionListModel> QuestionListStore { get; }
        public InMemoryStore<QuestionListOverviewModel> QuestionListOverviewStore { get; }
        public IMapper Mapper { get; }
        
        public Task Handle(QuestionListCreatedEvent message)
        {
            QuestionListStore.Save(message.Id, Mapper.Map(message.QuestionList, new QuestionListModel(message.Id)));
            QuestionListOverviewStore.Save(message.Id, Mapper.Map(message.QuestionList, new QuestionListOverviewModel(message.Id)));
            return Task.CompletedTask;
        }

        public Task Handle(QuestionListUpdatedEvent message)
        {
            QuestionListStore.Save(message.Id, Mapper.Map(message.QuestionList, new QuestionListModel(message.Id)));
            QuestionListOverviewStore.Save(message.Id, Mapper.Map(message.QuestionList, new QuestionListOverviewModel(message.Id)));
            return Task.CompletedTask;
        }
        
        public Task Handle(QuestionListDeletedEvent message)
        {
            QuestionListOverviewStore.GetById(message.Id).IfSome(list =>
            {
                list.IsDeleted = true;
                QuestionListOverviewStore.Save(message.Id, list);
            });
            return Task.CompletedTask;
        }
    }
}