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
    public class EmployeeOperations
    {
        /// <summary>
        /// Get all employees, not specific to Employee operations
        /// </summary>
        /// <returns></returns>
        public static List<Employees> GetAll()
        {
            var repo = new GenericRepository<Employees>();
            return (List<Employees>) repo.GetAll();
        }

        /// <summary>
        /// Get all employees with desired navigation properties strong typed
        /// </summary>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public static List<Employees> GetAll(params Expression<Func<Employees, object>>[] navigationProperties)
        {
            var repo = new GenericRepository<Employees>();
            return (List<Employees>)repo.GetAll(navigationProperties);
        }

        /// <summary>
        /// Get employee by primary key with optional navigation properties
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static async Task<Employees> GenericRepositoryFindAsync(int identifier, string[] paths)
        {
            var repo = new GenericRepository<Employees>();
            return await repo.GetTask(identifier, paths);
        }

        /// <summary>
        /// Get employee by primary key with all navigation properties
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static async Task<Employees> GenericRepositoryFindWithIncludesAsync(int identifier)
        {
            var repo = new GenericRepository<Employees>();
            return await repo.GetWithIncludesTask(identifier);
        }
        public static async Task<Employees> Get(int identifier, string[] navigationPaths = null)
        {
            using (var context = new NorthwindContext())
            {

                var employees = await context.Employees.FindAsync(identifier);

                if (navigationPaths == null) return employees;

                foreach (var navigation in context.Entry(employees).Navigations)
                {
                    await navigation.LoadAsync();
                }

                foreach (var path in navigationPaths)
                {
                    await context.Entry((object)employees).Reference(path).LoadAsync();
                }

                return employees;
            }


        }

        /// <summary>
        /// Get employee by primary key with all navigation properties
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static async Task<Employees> GetWithAllNavigationProperties(int identifier)
        {
            using (var context = new NorthwindContext())
            {

                var employees = await context.Employees.FindAsync(identifier);

                foreach (NavigationEntry navigation in context.Entry(employees).Navigations)
                {
                    await navigation.LoadAsync();
                }

                return employees;
            }

        }
        /// <summary>
        /// Get single Employees with no navigation properties
        /// </summary>
        /// <param name="keys">primary key to locate by</param>
        /// <returns>Employees if found, null if not found</returns>
        public static async Task<Employees> FindEmployeesAsync(object[] keys)
        {

            using (var context = new NorthwindContext())
            {
                var employees = await context.Employees.FindAsync(keys);
                return employees;
            }

        }
    }
}
