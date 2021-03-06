using System;
using System.Collections.Generic;
using LanguageExt;
using Rehearsal.Messages.QuestionList;

namespace Rehearsal.WebApi.QuestionList
{
    public interface IQuestionListRepository
    {
        IEnumerable<QuestionListOverviewModel> GetAll();
        IEnumerable<QuestionListOverviewModel> GetAllOrderedByTitle();
        Option<QuestionListModel> GetById(Guid id);
    }
}