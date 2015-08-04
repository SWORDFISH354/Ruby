using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Core.Common.Contracts;
using Core.Common.Core;
using GulfRuby.Business.Bootstrapper;
using GulRuby.Business.Entities;
using GulRuby.Data.Contracts.Repository_Interfaces;
using Microsoft.JScript;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GulfRuby.Data.Tests
{
    [TestClass]
    public class DataLayerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void test_repository_message()
        {
            var repositoryTest = new RepositoryTestClass();
            var airlines = repositoryTest.GetAirlines();
            Assert.IsTrue(airlines != null);
        }

        [TestMethod]
        public void test_repository_factory_message()
        {
            var repositoryTest = new RepositoryFactoryTestClass();
            var airlines = repositoryTest.GetAirlines();
            Assert.IsTrue(airlines != null);
        }

        //[TestMethod]
        //public void test_repository_mocking()
        //{
        //    var airlines = new List<Airline>()
        //                   {
        //                       new Airline() {ID = 2,Name = "Emirates",Code = "EK",AddedBy = "Sunil Samuel",AddedTime = DateTime.UtcNow},
        //                       new Airline() {ID = 3,Name = "Air India",Code = "AI",AddedBy = "Sunil Samuel",AddedTime = DateTime.UtcNow},
        //                   };
        //    Mock<IAirlineRepository> mockAirlineRepository = new Mock<IAirlineRepository>();
        //    mockAirlineRepository.Setup(obj => obj.Get()).Returns(airlines);
        //    var repositoryTest = new RepositoryTestClass(mockAirlineRepository.Object);
        //    var airlineList = repositoryTest.GetAirlines();
        //    Assert.IsTrue(airlineList.Count() > 1);
        //}


        [TestMethod]
        public void test_repository_factory_mocking()
        {
            var airlines = new List<Airline>()
                           {
                               new Airline() {ID = 2,Name = "Emirates",Code = "EK",AddedBy = "Sunil Samuel",AddedTime = DateTime.UtcNow},
                               new Airline() {ID = 3,Name = "Air India",Code = "AI",AddedBy = "Sunil Samuel",AddedTime = DateTime.UtcNow},
                           };

            Mock<IDataRepositoryFactory> mockAirlineRepository = new Mock<IDataRepositoryFactory>();
            mockAirlineRepository.Setup(obj => obj.GetDataRepository<IAirlineRepository>().Get()).Returns(airlines);
            
            
            
            var repositoryTest = new RepositoryFactoryTestClass(mockAirlineRepository.Object);
            var airlineList = repositoryTest.GetAirlines();
            Assert.IsTrue(airlineList.Count() > 1);
        }



        [TestMethod]
        public void test_repository_factory_mocking2()
        {
            var airlines = new List<Airline>()
                           {
                               new Airline() {ID = 2,Name = "Emirates",Code = "EK",AddedBy = "Sunil Samuel",AddedTime = DateTime.UtcNow},
                               new Airline() {ID = 3,Name = "Air India",Code = "AI",AddedBy = "Sunil Samuel",AddedTime = DateTime.UtcNow},
                           };

            Mock<IAirlineRepository> mockAirlineRepository = new Mock<IAirlineRepository>();
            mockAirlineRepository.Setup(obj => obj.Get()).Returns(airlines);

            Mock<IDataRepositoryFactory> mockAirlineFactoryRepository = new Mock<IDataRepositoryFactory>();
            mockAirlineFactoryRepository.Setup(obj => obj.GetDataRepository<IAirlineRepository>()).Returns(mockAirlineRepository.Object);



            var repositoryTest = new RepositoryFactoryTestClass(mockAirlineFactoryRepository.Object);
            var airlineList = repositoryTest.GetAirlines();
            Assert.IsTrue(airlineList.Count() > 1);
        }



        //[TestMethod]
        //public void test_repository_mocking_add()
        //{
        //    var airline = new Airline()
        //                   {
        //                       ID = 4,
        //                       Name = "Cathay Pacific",
        //                       Code = "CX",
        //                       AddedBy = "Sunil Samuel",
        //                       AddedTime = DateTime.UtcNow
        //                   };

        //    Mock<IAirlineRepository> mockAirlineRepository = new Mock<IAirlineRepository>();
        //    mockAirlineRepository.Setup(obj => obj.Add(airline)).Returns(airline);
        //    var repositoryTest = new RepositoryTestClass(mockAirlineRepository.Object);
        //    var airlineList = repositoryTest.AddAirlinesAirlines(airline);
        //    Assert.IsTrue(airlineList != null);
        //}

        [TestMethod]
        public void test_employee_repository_mocking_add()
        {
            var employee = new Employee()
            {
                Id = 0,
                Name = "Sunil Samuel    ",
                Position = "Developer"
            };

            Mock<IEmployeeRepository> mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(obj => obj.Add(employee)).Returns(employee);
            var repositoryTest = new RepositoryTestClass(mockEmployeeRepository.Object);
            var airlineList = repositoryTest.AddEmployee(employee);
            Assert.IsTrue(airlineList != null);
        }   


    }

    public class RepositoryTestClass
    {
        public RepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryTestClass( IEmployeeRepository employeeRepository)
        {
            
            _employeeRepository = employeeRepository;
        }

        [Import]
        IAirlineRepository _airlineRepository;

        [Import]
        IEmployeeRepository _employeeRepository;

        public IEnumerable<Airline> GetAirlines()
        {
            var airlines = _airlineRepository.Get();
            return airlines;
        }

        public Airline AddAirlinesAirlines(Airline airline)
        {

            _airlineRepository.Add(airline);
            return airline;
        }


        public IEnumerable<Employee> GetEmployees()
        {
            var employees = _employeeRepository.Get();
            return employees;
        }

        public Employee AddEmployee(Employee employee)
        {

            _employeeRepository.Add(employee);
            return employee;
        }






    }

    public class RepositoryFactoryTestClass
    {

        public RepositoryFactoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryFactoryTestClass(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        public IEnumerable<Airline> GetAirlines()
        {
            var repository = _dataRepositoryFactory.GetDataRepository<IAirlineRepository>();
            var airlines = repository.Get();
            return airlines;
        }



    }
}
