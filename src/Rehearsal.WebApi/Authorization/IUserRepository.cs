using System;
using System.Collections.Generic;
using LanguageExt;
using Rehearsal.Messages.Authorization;

namespace Rehearsal.WebApi.Authorization
{
    public interface IUserRepository
    {
        Option<UserModel> GetById(Guid id);
        IEnumerable<UserModel> GetAll();
        Option<UserModel> GetByUsername(string userName);
    }
}