/// Base repository interface
/// Codeora 2012
///

namespace Prestige.EF
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface for a database repository.
    /// </summary>
    /// <typeparam name="T">The model type.</typeparam>
    public interface IRepository<T> : IQueryable<T>
    {
        /// <summary>
        /// Singles the or default.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        T SingleOrDefault(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes entities using the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        void Delete(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Deletes all.
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Loads the property.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="selector">The selector.</param>
        void LoadProperty(T entity, Expression<Func<T, object>> selector);

        /// <summary>
        /// Saves the changes.
        /// </summary>
        void SaveChanges();
    }
}
