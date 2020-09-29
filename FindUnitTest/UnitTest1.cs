using System;
using System.Threading.Tasks;
using FindUnitTest.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthEntityLibrary.Classes;
using NorthEntityLibrary.Models;
using NorthEntityLibrary.Repositories;
using Shouldly;

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

            employee.FirstName.ShouldBe("Janet");
            employee.LastName.ShouldBe("Leverling");

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

            customer.CountryIdentifierNavigation.ShouldNotBeNull();
            customer.ContactTypeIdentifierNavigation.ShouldNotBeNull();

            // get customer w/o any navigation properties
            customer = await CustomerOperations.Get(3);

            
            customer.CompanyName.ShouldBe("Antonio Moreno Taquería");

            // get customer with all navigation properties
            customer = await CustomerOperations.GetWithAllNavigationProperties(3);


            customer.CountryIdentifierNavigation.ShouldNotBeNull();
            customer.ContactTypeIdentifierNavigation.ShouldNotBeNull();
            customer.Orders.ShouldNotBeNull();
            customer.Contact.ShouldNotBeNull();

            
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


            employee.CountryIdentifierNavigation.ShouldNotBeNull();
            employee.ContactTypeIdentifierNavigation.ShouldNotBeNull();

            // get customer w/o any navigation properties
            employee = await EmployeeOperations.Get(3);

            employee.FirstName.ShouldBe("Janet");
            employee.LastName.ShouldBe("Leverling");

            // get customer with all navigation properties
            employee = await EmployeeOperations.GetWithAllNavigationProperties(3);

            employee.CountryIdentifierNavigation.ShouldNotBeNull();
            employee.ContactTypeIdentifierNavigation.ShouldNotBeNull();
            employee.Orders.ShouldNotBeNull();

        }

        /// <summary>
        /// Test generic FindAsync for Customers
        /// </summary>
        /// <returns></returns>
        [TestMethod, TestTraits(Trait.FindGeneric)]
        public async Task GenericCustomersFindTask()
        {
            var customer = await CustomerOperations.GenericRepositoryFindAsync(3, new []{ "Contact" });

            customer.CompanyName.ShouldBe("Antonio Moreno Taquería");
            customer.Contact.ShouldNotBeNull();

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

            employee.CountryIdentifierNavigation.ShouldNotBeNull();
            employee.ContactTypeIdentifierNavigation.ShouldNotBeNull();

        }

        [TestMethod, TestTraits(Trait.FindGeneric)]
        public async Task EmployeeGenericRepositoryFindWithIncludesAsync()
        {
            var employee = await EmployeeOperations.GenericRepositoryFindWithIncludesAsync(3);

            employee.CountryIdentifierNavigation.ShouldNotBeNull();
            employee.ContactTypeIdentifierNavigation.ShouldNotBeNull();
            employee.Orders.ShouldNotBeNull();

        }
        [TestMethod, TestTraits(Trait.FindGeneric)]
        public async Task CustomersGenericRepositoryFindWithIncludesAsync()
        {
            var customer = await CustomerOperations.GenericRepositoryFindWithIncludesAsync(3);

            customer.CountryIdentifierNavigation.ShouldNotBeNull();
            customer.Contact.ShouldNotBeNull();
            customer.ContactTypeIdentifierNavigation.ShouldNotBeNull();
            customer.Orders.ShouldNotBeNull();

        }

        [TestMethod, TestTraits(Trait.FindGeneric)]
        public async Task OrderGenericRepositoryFindWithIncludesAsync()
        {
            var order = await OrdersOperations.GenericRepositoryFindWithIncludesAsync(10249);

            order.CustomerIdentifierNavigation.ShouldNotBeNull();
            order.OrderDetails.ShouldNotBeNull();
            order.Employee.ShouldNotBeNull();
            order.ShipViaNavigation.ShouldNotBeNull();

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


            order.CustomerIdentifierNavigation.ShouldNotBeNull();
            order.Employee.ShouldNotBeNull();
            order.ShipViaNavigation.ShouldNotBeNull();

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
