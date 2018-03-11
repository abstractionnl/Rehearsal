using System;
using Bogus.DataSets;

namespace Rehearsal.Xunit
{
    public static class FakerExtensions
    {
        public static DateTime BetweenDaysAgo(this Date faker, int minimumDaysAgo, int maximumDaysAgo) => 
            faker.Between(DateTime.UtcNow.AddDays(maximumDaysAgo), DateTime.UtcNow.AddDays(minimumDaysAgo));
    }
}