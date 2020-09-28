using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NorthEntityLibrary.Interfaces
{
    /// <summary>
    /// For demoing FindAsync in a generic repository
    /// </summary>
    /// <typeparam name="TEntity">Model to work with</typeparam>
    public interface IGenericOperations<TEntity> where TEntity : class
    {
        Task<TEntity> GetTask(int identifier, string[] paths = null);
        TEntity Get(int identifiers, string[] paths = null);
        Task<TEntity> GetWithIncludesTask(int id);
        TEntity GetWithIncludes(int identifier);
        IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);
    }
}