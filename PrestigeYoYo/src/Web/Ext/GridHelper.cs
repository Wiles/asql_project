/// Extension methods to help with data grids
/// Codeora 2012
///

namespace Prestige
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Extension helper methods for building grid data.
    /// </summary>
    public static class GridHelper
    {
        /// <summary>
        /// Sorts the IQueryable.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="data">The searchString.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>A sorted IQueryable.</returns>
        public static IQueryable<T> Sort<T>(
                this IQueryable<T> data,
                string fieldName,
                string sortOrder)
        {
            if (string.IsNullOrWhiteSpace(fieldName)
                || string.IsNullOrWhiteSpace(sortOrder))
            {
                return data;
            }

            var param = Expression.Parameter(typeof(T), "i");
            var type = Expression.Property(param, fieldName).Type;

            if (type == typeof(Guid))
            {
                Expression conversion =
                        Expression.Convert(
                                Expression.Property(param, fieldName),
                                typeof(Guid));

                var sortExpression = Expression.Lambda<Func<T, Guid>>(conversion, param);

                return sortOrder == "desc" ? data.OrderByDescending<T, Guid>(sortExpression)
                    : data.OrderBy<T, Guid>(sortExpression);
            }
            else
            {
                Expression conversion =
                        Expression.Convert(
                                Expression.Property(param, fieldName),
                                typeof(object));

                var sortExpression = Expression.Lambda<Func<T, object>>(conversion, param);

                return sortOrder == "desc" ? data.OrderByDescending(sortExpression)
                    : data.OrderBy(sortExpression);
            }
        }

        /// <summary>
        /// Searches the IQueryable.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="data">The IQueryable.</param>
        /// <param name="searchField">The search field.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns>The search results.</returns>
        public static IQueryable<T> Search<T>(
                this IQueryable<T> data,
                string searchField,
                string searchString)
        {
            return data.Where(t =>
                    (typeof(T).GetProperty(searchField)
                                    .GetValue(t, null) == null ? false
                    : typeof(T).GetProperty(searchField)
                                    .GetValue(t, null)
                                    .ToString()
                                    .ToLower()
                                    .Contains(searchString.ToLower())));
        }
    }
}