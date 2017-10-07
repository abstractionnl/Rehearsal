using Bogus;

namespace Rehearsal.Tests
{
    public abstract class TestBase
    {
        protected Faker Faker { get;  }

        protected TestBase()
        {
            Faker = new Faker();
        }
    }
}