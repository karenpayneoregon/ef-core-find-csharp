using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NorthEntityLibrary.Contexts;
using NorthEntityLibrary.Models;
using NorthEntityLibrary.Repositories;

namespace NorthEntityLibrary.Classes
{
    public class ProductsOperations
    {
        /// <summary>
        /// Get Product with no navigation properties or
        /// with one or more navigation properties.
        /// </summary>
        /// <param name="identifier">Primary key to find</param>
        /// <param name="paths">Optional navigation properties</param>
        /// <returns>Product</returns>
        public static async Task<Products> GenericRepositoryFindAsync(int identifier, string[] paths)
        {
            var repo = new GenericRepository<Products>();
            return await repo.GetTask(identifier, paths);
        }

        /// <summary>
        /// Get product with all navigation properties
        /// </summary>
        /// <param name="identifier">Primary key to find</param>
        /// <returns>A Product associated with the passed in primary key</returns>
        public static async Task<Products> GenericRepositoryFindWithIncludesAsync(int identifier)
        {
            var repo = new GenericRepository<Products>();
            return await repo.GetWithIncludesTask(identifier);
        }
        /// <summary>
        /// Get a product by primary key with no navigation, one or more navigation properties
        /// </summary>
        /// <param name="identifier">Primary key to find</param>
        /// <param name="navigationPaths">Optional navigation properties</param>
        /// <returns>Product</returns>
        public static async Task<Products> Get(int identifier, string[] navigationPaths = null)
        {
            using (var context = new NorthwindContext())
            {

                var product = await context.Products.FindAsync(identifier);

                if (navigationPaths == null) return product;

                foreach (var navigation in context.Entry(product).Navigations)
                {
                    await navigation.LoadAsync();
                }

                foreach (var path in navigationPaths)
                {
                    await context.Entry((object)product).Reference(path).LoadAsync();
                }

                return product;
            }


        }

        /// <summary>
        /// Get a Product by primary key
        /// </summary>
        /// <param name="identifier">Primary key used to find product</param>
        /// <returns>Product</returns>
        public static async Task<Products> GetWithAllNavigationProperties(int identifier)
        {
            using (var context = new NorthwindContext())
            {

                var product = await context.Products.FindAsync(identifier);

                foreach (NavigationEntry navigation in context.Entry(product).Navigations)
                {
                    await navigation.LoadAsync();
                }

                return product;
            }

        }
        /// <summary>
        /// Get single Customers with no navigation properties
        /// </summary>
        /// <param name="keys">primary key to locate by</param>
        /// <returns>Employees if found, null if not found</returns>
        public static async Task<Products> FindCustomersAsync(object[] keys)
        {

            using (var context = new NorthwindContext())
            {
                var product = await context.Products.FindAsync(keys);
                return product;
            }

        }
    }
}
