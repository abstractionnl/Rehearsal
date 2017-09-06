using System;
using System.Collections.Generic;
using LanguageExt;
using Rehearsal.Messages;
using Rehearsal.WebApi;

namespace Rehearsal.Data
{
    public class QuestionListRepository : IQuestionListRepository
    {
        public QuestionListRepository(InMemoryStore<QuestionListModel> questionListStore, InMemoryStore<QuestionListOverviewModel> questionListOverviewStore)
        {
            QuestionListStore = questionListStore;
            QuestionListOverviewStore = questionListOverviewStore;
        }

        private InMemoryStore<QuestionListModel> QuestionListStore { get; }
        private InMemoryStore<QuestionListOverviewModel> QuestionListOverviewStore { get; }

        public IEnumerable<QuestionListOverviewModel> GetAll() => QuestionListOverviewStore.All;

        public Option<QuestionListModel> GetById(Guid id) => QuestionListStore.GetById(id);
    }
}