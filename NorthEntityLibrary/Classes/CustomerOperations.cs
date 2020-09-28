using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NorthEntityLibrary.Contexts;
using NorthEntityLibrary.Models;
using NorthEntityLibrary.Repositories;

namespace NorthEntityLibrary.Classes
{
    public class CustomerOperations
    {
        /// <summary>
        /// Generic find, not specific to customers
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static async Task<Customers> GenericRepositoryFindAsync(int identifier, string[] paths)
        {
            var repo = new GenericRepository<Customers>();
            return await repo.GetTask(identifier, paths);
        }

        /// <summary>
        /// Get all customers without navigation properties
        /// </summary>
        /// <returns></returns>
        public static List<Customers> GetAll()
        {
            var repo = new GenericRepository<Customers>();
            return (List<Customers>) repo.GetAll();
        }

        /// <summary>
        /// Get all Customers with desired navigation properties
        /// </summary>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public static List<Customers> GetAll(params Expression<Func<Customers, object>>[] navigationProperties)
        {
            var repo = new GenericRepository<Customers>();
            return (List<Customers>)repo.GetAll(navigationProperties);
        }

        /// <summary>
        /// Get customer by primary key with all navigation properties
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static async Task<Customers> GenericRepositoryFindWithIncludesAsync(int identifier)
        {
            var repo = new GenericRepository<Customers>();
            return await repo.GetWithIncludesTask(identifier);
        }

        /// <summary>
        /// Get customer by primary key and optional one or more navigation properties
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="navigationPaths"></param>
        /// <returns></returns>
        public static async Task<Customers> Get(int identifier, string[] navigationPaths = null)
        {
            using (var context = new NorthwindContext())
            {

                var customer = await context.Customers.FindAsync(identifier);

                if (navigationPaths == null) return customer;

                foreach (var navigation in context.Entry(customer).Navigations)
                {
                    await navigation.LoadAsync();
                }

                foreach (var path in navigationPaths)
                {
                    await context.Entry((object)customer).Reference(path).LoadAsync();
                }

                return customer;
            }


        }
        /// <summary>
        /// Get customer by primary key with all navigation properties.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static async Task<Customers> GetWithAllNavigationProperties(int identifier)
        {
            using (var context = new NorthwindContext())
            {

                var customer = await context.Customers.FindAsync(identifier);

                foreach (NavigationEntry navigation in context.Entry(customer).Navigations)
                {
                    await navigation.LoadAsync();
                }

                return customer;
            }

        }
        /// <summary>
        /// Get single Customers with no navigation properties
        /// </summary>
        /// <param name="keys">primary key to locate by</param>
        /// <returns>Employees if found, null if not found</returns>
        public static async Task<Customers> FindCustomersAsync(object[] keys)
        {

            using (var context = new NorthwindContext())
            {
                var customer = await context.Customers.FindAsync(keys);
                return customer;
            }

        }
    }
}
