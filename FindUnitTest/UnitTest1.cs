using System;
using System.Threading.Tasks;
using FindUnitTest.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthEntityLibrary.Classes;
using NorthEntityLibrary.Models;
using NorthEntityLibrary.Repositories;

namespace FindUnitTest
{
    [TestClass]
    public class UnitTest1 : BaseClass
    {
        [TestMethod, TestTraits(Trait.Find)]
        public async Task CustomersFindSimpleTask()
        {
            var customer = await CustomerOperations.FindCustomersAsync(new object[] { 3 });

            Assert.AreEqual(customer.CompanyName, "Antonio Moreno Taquería");

        }

        [TestMethod, TestTraits(Trait.Find)]
        public async Task EmployeesFindSimpleTask()
        {
            var employee = await EmployeeOperations.FindEmployeesAsync(new object[] { 3 });

            Assert.AreEqual(employee.FirstName, "Janet");
            Assert.AreEqual(employee.LastName, "Leverling");

        }

        /// <summary>
        /// Find with navigation(s) and without navigation properties
        /// </summary>
        /// <returns></returns>
        [TestMethod, TestTraits(Trait.Find)]
        public async Task FindCustomersWithIncludesTask()
        {
            // specify which navigation properties to include
            var customer = await CustomerOperations.Get(3, new[]
            {
                "CountryIdentifierNavigation",
                "ContactTypeIdentifierNavigation"
            });

            Assert.IsTrue(
                customer.CountryIdentifierNavigation != null &&
                customer.ContactTypeIdentifierNavigation != null);

            // get customer w/o any navigation properties
            customer = await CustomerOperations.Get(3);

            Assert.AreEqual(customer.CompanyName, "Antonio Moreno Taquería");

            // get customer with all navigation properties
            customer = await CustomerOperations.GetWithAllNavigationProperties(3);

            Assert.IsTrue(
                customer.CountryIdentifierNavigation != null &&
                customer.ContactTypeIdentifierNavigation != null &
                customer.Orders != null &&
                customer.Contact != null);


        }
        /// <summary>
        /// Find with navigation(s) and without navigation properties
        /// </summary>
        /// <returns></returns>
        [TestMethod, TestTraits(Trait.Find)]
        public async Task FindEmployeesWithIncludesTask()
        {
            // specify which navigation properties to include
            var employee = await EmployeeOperations.Get(3, new[]
            {
                "ContactTypeIdentifierNavigation",
                "CountryIdentifierNavigation"
            });

            Assert.IsTrue(
                employee.CountryIdentifierNavigation != null &&
                employee.ContactTypeIdentifierNavigation != null);

            // get customer w/o any navigation properties
            employee = await EmployeeOperations.Get(3);

            Assert.AreEqual(employee.FirstName, "Janet");
            Assert.AreEqual(employee.LastName, "Leverling");

            // get customer with all navigation properties
            employee = await EmployeeOperations.GetWithAllNavigationProperties(3);

            Assert.IsTrue(
                employee.CountryIdentifierNavigation != null &&
                employee.ContactTypeIdentifierNavigation != null &
                employee.Orders != null);

        }

        /// <summary>
        /// Test generic FindAsync for Customers
        /// </summary>
        /// <returns></returns>
        [TestMethod, TestTraits(Trait.FindGeneric)]
        public async Task GenericCustomersFindTask()
        {
            var customer = await CustomerOperations.GenericRepositoryFindAsync(3, new []{ "Contact" });

            Assert.AreEqual(customer.CompanyName, "Antonio Moreno Taquería");

            Assert.IsTrue(customer.Contact != null);
        }
        /// <summary>
        /// Test generic FindAsync for Employee
        /// </summary>
        /// <returns></returns>
        [TestMethod, TestTraits(Trait.Find)]
        public async Task GenericEmployeesFindTask()
        {
            var employee = await EmployeeOperations.GenericRepositoryFindAsync(3,new[]
            {
                "CountryIdentifierNavigation",
                "ContactTypeIdentifierNavigation"
            });

            Assert.IsTrue(employee.ContactTypeIdentifierNavigation != null &&
                          employee.CountryIdentifierNavigation != null);

        }

        [TestMethod, TestTraits(Trait.FindGeneric)]
        public async Task EmployeeGenericRepositoryFindWithIncludesAsync()
        {
            var employee = await EmployeeOperations.GenericRepositoryFindWithIncludesAsync(3);

            Assert.IsTrue(
                employee.CountryIdentifierNavigation != null &&
                employee.ContactTypeIdentifierNavigation != null && employee.Orders != null);

        }
        [TestMethod, TestTraits(Trait.FindGeneric)]
        public async Task CustomersGenericRepositoryFindWithIncludesAsync()
        {
            var customer = await CustomerOperations.GenericRepositoryFindWithIncludesAsync(3);

            Assert.IsTrue(
                customer.CountryIdentifierNavigation != null &&
                customer.ContactTypeIdentifierNavigation != null && 
                customer.Contact != null &&
                customer.Orders != null);

        }

        [TestMethod, TestTraits(Trait.FindGeneric)]
        public async Task OrderGenericRepositoryFindWithIncludesAsync()
        {
            var order = await OrdersOperations.GenericRepositoryFindWithIncludesAsync(10249);

            Assert.IsTrue(
                order.CustomerIdentifierNavigation != null &&
                order.Employee != null &&
                order.OrderDetails != null &&
                order.ShipViaNavigation != null);

        }
        [TestMethod, TestTraits(Trait.FindGeneric)]
        public async Task GenericOrderFindTask()
        {
            var order = await OrdersOperations.GenericRepositoryFindAsync(10249, new[]
            {
                "CustomerIdentifierNavigation",
                "ShipViaNavigation",
                "Employee"
            });

            Assert.IsTrue(order.Employee != null &&
                          order.CustomerIdentifierNavigation != null && 
                          order.ShipViaNavigation != null);

        }

        /// <summary>
        /// Not a true test, simply a demonstration for a generic find all
        /// </summary>
        [TestMethod, TestTraits(Trait.FindAll)]
        public void CustomersGetAllTestMethod()
        {
            var genericRepository = new GenericRepository<Customers>();

            var allCustomersWithoutNavigation = CustomerOperations.GetAll();

            var allCustomersWithNavigation = CustomerOperations.GetAll(
                customers => customers.Orders,
                customers => customers.Contact,
                customers => customers.ContactTypeIdentifierNavigation,
                customers => customers.CountryIdentifierNavigation);
        }
        /// <summary>
        /// Not a true test, simply a demonstration for a generic find all
        /// </summary>
        [TestMethod, TestTraits(Trait.FindAll)]
        public void EmployeeGetAllTestMethod()
        {
            var allEmployeesWithoutNavigation = EmployeeOperations.GetAll();

            var allEmployeeWithNavigation = EmployeeOperations.GetAll(
                customers => customers.Orders,
                customers => customers.ContactTypeIdentifierNavigation,
                customers => customers.CountryIdentifierNavigation);
        }

    }
}
