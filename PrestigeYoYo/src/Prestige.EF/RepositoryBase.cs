/// Repository base
/// Codeora 2012
///

namespace Prestige.EF
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Base implementation of IRepository
    /// </summary>
    /// <typeparam name="T">The model type.</typeparam>
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="RepositoryBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RepositoryBase(DbContext context)
        {
            this.Context = context;
                this.ObjectContext =
                    (context as IObjectContextAdapter).ObjectContext;
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the
        /// expression tree associated with this instance of
        /// <see cref="T:System.Linq.IQueryable"/> is executed.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Type"/> that represents the type of the
        ///     element(s) that are returned when the expression tree
        ///     associated with this object is executed.
        /// </returns>
        public Type ElementType
        {
            get { return (this.Context.Set<T>() as IQueryable).ElementType; }
        }

        /// <summary>
        /// Gets the expression tree that is associated with 
        /// the instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Linq.Expressions.Expression"/>
        ///     that is associated with this instance of
        ///     <see cref="T:System.Linq.IQueryable"/>.
        /// </returns>
        public Expression Expression
        {
            get { return (this.Context.Set<T>() as IQueryable).Expression; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Linq.IQueryProvider"/>
        ///     that is associated with this data source.
        /// </returns>
        public IQueryProvider Provider
        {
            get { return (this.Context.Set<T>() as IQueryable).Provider; }
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        protected DbContext Context { get; set; }

        /// <summary>
        /// Gets or sets the object context.
        /// </summary>
        /// <value>
        /// The object context.
        /// </value>
        protected ObjectContext ObjectContext { get; set; }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified
        /// condition or a default value if no such element exists.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     The only element or the default value if one does not exist.
        /// </returns>
        public T SingleOrDefault(Expression<Func<T, bool>> expression)
        {
            return (this as IQueryable<T>).SingleOrDefault(expression);
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(T entity)
        {
            this.Context.Set<T>().Add(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            this.Context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Deletes entities using the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public void Delete(Expression<Func<T, bool>> expression)
        {
            (this as IRepository<T>).Where(expression).ToList().ForEach(
                    entity => (this as IRepository<T>).Delete(entity));
        }

        /// <summary>
        /// Deletes all.
        /// </summary>
        public void DeleteAll()
        {
            (this as IRepository<T>).ToList().ForEach(
                    entity => (this as IRepository<T>).Delete(entity));
        }

        /// <summary>
        /// Loads the property.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="selector">The selector.</param>
        public void LoadProperty(
                T entity,
                Expression<Func<T, object>> selector)
        {
            ObjectStateEntry stateEntry = null;
            this.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(
                    entity,
                    out stateEntry);

            if (stateEntry != null && stateEntry.State != EntityState.Detached
                    && stateEntry.State != EntityState.Added)
            {
                ObjectContext.LoadProperty<T>(entity, selector);
            }
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1"/>
        ///     that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (this.Context.Set<T>() as IQueryable<T>).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"/> object
        ///     that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return (this as IQueryable<T>).GetEnumerator();
        }
    }
}
