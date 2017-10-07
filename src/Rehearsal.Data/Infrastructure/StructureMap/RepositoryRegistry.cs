using Rehearsal.Messages;
using Rehearsal.WebApi;
using StructureMap;

namespace Rehearsal.Data.Infrastructure.StructureMap
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For(typeof(InMemoryStore<>)).Singleton().Use(typeof(InMemoryStore<>));

            For<IQuestionListRepository>().Use<QuestionListRepository>();
            For<IUserRepository>().Use<UserRepository>();
            For<IRehearsalSessionRepository>().Use<RehearsalSessionRepository>();
        }
    }
}