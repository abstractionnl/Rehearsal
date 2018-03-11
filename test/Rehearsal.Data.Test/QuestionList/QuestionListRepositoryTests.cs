using System;
using Bogus;
using NFluent;
using Rehearsal.Data.Infrastructure;
using Rehearsal.Data.QuestionList;
using Rehearsal.Messages.QuestionList;
using Rehearsal.Xunit;
using Xunit;

namespace Rehearsal.Data.Test.QuestionList
{
    public class QuestionListRepositoryTests
    {
        public Faker Faker { get; }
        
        public QuestionListRepositoryTests()
        {
            Faker = new Faker();
            QuestionListStore = new InMemoryStore<QuestionListModel>();
            QuestionListOverviewStore = new InMemoryStore<QuestionListOverviewModel>();
            Repository = new QuestionListRepository(QuestionListStore, QuestionListOverviewStore);
        }
        
        public InMemoryStore<QuestionListOverviewModel> QuestionListOverviewStore { get; }
        public InMemoryStore<QuestionListModel> QuestionListStore { get; }
        private QuestionListRepository Repository { get; }

        [Fact]
        public void CanGetQuestionListFromStore()
        {
            var questionList = Faker.QuestionListModel();
            QuestionListStore.Save(questionList.Id, questionList);

            Check.That(Repository.GetById(questionList.Id))
                .IsSome().Which.IsEqualTo(questionList);
        }

        [Fact]
        public void QuestionListIsInOverview()
        {
            var overviewModel = Faker.QuestionListOverviewModel();
            
            QuestionListOverviewStore.Save(overviewModel.Id, overviewModel);

            Check.That(Repository.GetAll())
                .Contains(overviewModel);
        }
        
        [Fact]
        public void DeletedQuestionListIsNotInOverview()
        {
            var overviewModel = Faker.QuestionListOverviewModel();
            overviewModel.IsDeleted = true;
            
            QuestionListOverviewStore.Save(overviewModel.Id, overviewModel);

            Check.That(Repository.GetAll())
                .Not.Contains(overviewModel);
        }

        [Fact]
        public void QuestionListsAreOrdered()
        {
            var overviewModel = Faker.QuestionListOverviewModel(title: "A");
            var overviewModel2 = Faker.QuestionListOverviewModel(title: "C");
            var overviewModel3 = Faker.QuestionListOverviewModel(title: "B");
            
            QuestionListOverviewStore.Save(overviewModel.Id, overviewModel);
            QuestionListOverviewStore.Save(overviewModel2.Id, overviewModel2);
            QuestionListOverviewStore.Save(overviewModel3.Id, overviewModel3);

            Check.That(Repository.GetAllOrderedByTitle())
                .ContainsExactly(overviewModel, overviewModel3, overviewModel2);
        }
    }
}