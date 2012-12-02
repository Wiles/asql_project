///
///
///

namespace ServiceTests
{
    using System.Linq;
    using Moq;

    /// <summary>
    /// Extensions for Moq.
    /// </summary>
    public static class MoqExt
    {
        /// <summary>
        /// Setups the IQueryable.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="queryable">The queryable.</param>
        public static void SetupIQueryable<T>(this Mock<T> mock, IQueryable queryable) where T : class, IQueryable
        {
            mock.Setup(r => r.GetEnumerator()).Returns(queryable.GetEnumerator());
            mock.Setup(r => r.Provider).Returns(queryable.Provider);
            mock.Setup(r => r.ElementType).Returns(queryable.ElementType);
            mock.Setup(r => r.Expression).Returns(queryable.Expression);
        }
    }
}
