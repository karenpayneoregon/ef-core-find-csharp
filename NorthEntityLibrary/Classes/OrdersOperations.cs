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
    public class OrdersOperations
    {
        /// <summary>
        /// Get all orders, not specific to Employee operations
        /// </summary>
        /// <returns></returns>
        public static List<Orders> GetAll()
        {
            var repo = new GenericRepository<Orders>();
            return (List<Orders>)repo.GetAll();
        }
        public static List<Orders> GetAll(params Expression<Func<Orders, object>>[] navigationProperties)
        {
            var repo = new GenericRepository<Orders>();
            return (List<Orders>)repo.GetAll(navigationProperties);
        }

        /// <summary>
        /// Get Order with no navigation properties or
        /// with one or more navigation properties.
        /// </summary>
        /// <param name="identifier">Primary key to find</param>
        /// <param name="paths">Optional navigation properties</param>
        /// <returns>Order</returns>
        public static async Task<Orders> GenericRepositoryFindAsync(int identifier, string[] paths)
        {
            var repo = new GenericRepository<Orders>();
            return await repo.GetTask(identifier, paths);
        }

        /// <summary>
        /// Get an Order with all navigation properties
        /// </summary>
        /// <param name="identifier">Primary key to find</param>
        /// <returns>An Order associated with the passed in primary key</returns>
        public static async Task<Orders> GenericRepositoryFindWithIncludesAsync(int identifier)
        {
            var repo = new GenericRepository<Orders>();
            return await repo.GetWithIncludesTask(identifier);
        }
        /// <summary>
        /// Get an Order by primary key with no navigation, one or more navigation properties
        /// </summary>
        /// <param name="identifier">Primary key to find</param>
        /// <param name="navigationPaths">Optional navigation properties</param>
        /// <returns>Order</returns>
        public static async Task<Orders> Get(int identifier, string[] navigationPaths = null)
        {
            using (var context = new NorthwindContext())
            {

                var order = await context.Orders.FindAsync(identifier);

                if (navigationPaths == null) return order;

                foreach (var navigation in context.Entry(order).Navigations)
                {
                    await navigation.LoadAsync();
                }

                foreach (var path in navigationPaths)
                {
                    await context.Entry((object)order).Reference(path).LoadAsync();
                }

                return order;
            }


        }
        /// <summary>
        /// Get a Order by primary key
        /// </summary>
        /// <param name="identifier">Primary key used to find Order</param>
        /// <returns>Order</returns>
        public static async Task<Orders> GetWithAllNavigationProperties(int identifier)
        {
            using (var context = new NorthwindContext())
            {

                var order = await context.Orders.FindAsync(identifier);

                foreach (NavigationEntry navigation in context.Entry(order).Navigations)
                {
                    await navigation.LoadAsync();
                }

                return order;
            }

        }

        /// <summary>
        /// Get single Customers with no navigation properties
        /// </summary>
        /// <param name="keys">primary key to locate by</param>
        /// <returns>Employees if found, null if not found</returns>
        public static async Task<Orders> FindOrderAsync(object[] keys)
        {

            using (var context = new NorthwindContext())
            {
                var order = await context.Orders.FindAsync(keys);
                return order;
            }

        }

    }
}
