using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NorthEntityLibrary.Contexts;
using NorthEntityLibrary.Interfaces;
using NorthEntityLibrary.Models;

namespace NorthEntityLibrary.Repositories
{
    /// <summary>
    /// Popular generic method to include navigation properties
    /// </summary>
    /// <typeparam name="TEntity">Model</typeparam>
    public class GenericRepository<TEntity> : IGenericOperations<TEntity> where TEntity : class
    {
        private readonly NorthwindContext _context;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initialize DbContext and Model
        /// </summary>
        public GenericRepository()
        {
            _context = new NorthwindContext();
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Get entity by primary key
        /// </summary>
        /// <param name="identifier">primary key to find</param>
        /// <param name="references">empty, one or more navigation property by name</param>
        /// <returns>Entity if found along with navigation items if specified</returns>
        public async Task<TEntity> GetTask(int identifier, string[] references = null)
        {
            var model = await _dbSet.FindAsync(identifier);

            if (references == null) return model;

            foreach (var reference in references)
            {
                _context.Entry((object)model).Reference(reference).Load();
            }

            return model;

        }
        /// <summary>
        /// Get entity by primary key
        /// </summary>
        /// <param name="identifier">primary key to find</param>
        /// <param name="references">empty, one or more navigation property by name</param>
        /// <returns>Entity if found along with navigation items if specified</returns>
        public TEntity Get(int identifier, string[] references = null)
        {
            var model = _dbSet.Find(identifier);

            if (references == null) return model;

            foreach (var reference in references)
            {
                _context.Entry((object)model).Reference(reference).Load();
            }

            return model;

        }
        /// <summary>
        /// Get entity by primary key
        /// </summary>
        /// <param name="identifier">primary key to find</param>
        /// <returns>Entity if found all navigation(s) are included</returns>
        public async Task<TEntity> GetWithIncludesTask(int identifier)
        {
            var model = await _dbSet.FindAsync(identifier);

            foreach (NavigationEntry navigation in _context.Entry(model).Navigations)
            {
                await navigation.LoadAsync();
            }

            return model;

        }
        /// <summary>
        /// Get entity by primary key
        /// </summary>
        /// <param name="identifier">primary key to find</param>
        /// <returns>Entity if found all navigation(s) are included</returns>
        public TEntity GetWithIncludes(int identifier)
        {
            var model = _dbSet.Find(identifier);

            foreach (NavigationEntry navigation in _context.Entry(model).Navigations)
            {
                navigation.Load();
            }

            return model;

        }
        /// <summary>
        /// Get all with no navigation properties, one or more navigation properties
        /// </summary>
        /// <param name="navigationProperties">Navigation properties</param>
        /// <returns>List of TEntity</returns>
        public virtual IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;

            using (var context = new NorthwindContext())
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();

                foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include(navigationProperty);
                }

                list = dbQuery
                    .AsNoTracking()
                    .ToList<TEntity>();
            }

            return list;

        }
       
    }
}
