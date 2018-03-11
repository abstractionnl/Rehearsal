using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Rehearsal.Data.Infrastructure;
using Rehearsal.Messages;
using Rehearsal.Messages.QuestionList;
using Rehearsal.WebApi;
using Rehearsal.WebApi.QuestionList;

namespace Rehearsal.Data.QuestionList
{
    public class QuestionListRepository : IQuestionListRepository
    {
        public QuestionListRepository(InMemoryStore<QuestionListModel> questionListStore, InMemoryStore<QuestionListOverviewModel> questionListOverviewStore)
        {
            QuestionListStore = questionListStore ?? throw new ArgumentNullException(nameof(questionListStore));
            QuestionListOverviewStore = questionListOverviewStore ?? throw new ArgumentNullException(nameof(questionListOverviewStore));
        }

        private InMemoryStore<QuestionListModel> QuestionListStore { get; }
        private InMemoryStore<QuestionListOverviewModel> QuestionListOverviewStore { get; }

        public IEnumerable<QuestionListOverviewModel> GetAll() => 
            QuestionListOverviewStore.All.Where(x => !x.IsDeleted);

        public IEnumerable<QuestionListOverviewModel> GetAllOrderedByTitle() =>
            QuestionListOverviewStore.All.Where(x => !x.IsDeleted).OrderBy(x => x.Title);

        public Option<QuestionListModel> GetById(Guid id) => QuestionListStore.GetById(id);
    }
}