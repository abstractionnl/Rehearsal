using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Rehearsal.Data.Infrastructure;
using Rehearsal.Messages;
using Rehearsal.WebApi;

namespace Rehearsal.Data
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(InMemoryStore<UserModel> userStore)
        {
            UserStore = userStore;
        }

        private InMemoryStore<UserModel> UserStore { get; }

        public Option<UserModel> GetById(Guid id) => UserStore.GetById(id);
        public IEnumerable<UserModel> GetAll() => UserStore.All;

        public Option<UserModel> GetByUsername(string userName) =>
            UserStore.All.Where(x => x.UserName == userName).HeadOrNone();
    }
}