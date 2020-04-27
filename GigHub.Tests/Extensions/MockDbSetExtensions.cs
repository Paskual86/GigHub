using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Tests.Extensions
{
    /*
     https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking
     */

    public static class MockDbSetExtensions
    {
        public static void SetSource<T>(this Mock<DbSet<T>> mock, IList<T> source) where T : class
        {
            var data = source.AsQueryable();

            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }
    }
}
