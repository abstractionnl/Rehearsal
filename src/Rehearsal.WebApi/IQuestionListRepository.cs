using System;
using System.Collections.Generic;
using LanguageExt;
using Rehearsal.Messages;

namespace Rehearsal.WebApi
{
    public interface IQuestionListRepository
    {
        IEnumerable<QuestionListOverviewModel> GetAll();
        Option<QuestionListModel> GetById(Guid id);
    }

    public interface IUserRepository
    {
        Option<UserModel> GetById(Guid id);
        IEnumerable<UserModel> GetAll();
        Option<UserModel> GetByUsername(string userName);
    }
}