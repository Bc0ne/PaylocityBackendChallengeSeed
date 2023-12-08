using Api.Services;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System;
using Api.Models;
using NUnit.Framework;
using Api.Abstrations;

namespace ApiTests.UnitTests
{
    [TestFixture]
    public class PaycheckCalculationServiceTests
    {
        private readonly IPaycheckCalculationService _paycheckCalculationService;

        public PaycheckCalculationServiceTests()
        {
            _paycheckCalculationService = new PaycheckCalculationService();
        }

        [Test]
        public void CalculateTotalDeduction_EmployeeIsNull_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => _paycheckCalculationService.CalculateEmployeePaycheck(null));
        }

        [Test]
        public void CalculateTotalDeduction_EmployeeBelowThreshold_NoAdditionalCost()
        {
            Employee employee = new Employee
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            };

            decimal result = _paycheckCalculationService.CalculateEmployeePaycheck(employee);

            Assert.Equals(1000m * 26, result); 
        }

        [Test]
        public void CalculateTotalDeduction_EmployeeAboveThreshold_AdditionalEmployeeCost()
        {
            Employee employee = new Employee
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 90000m,
                DateOfBirth = new DateTime(1984, 12, 30),
                Dependents = new List<Dependent>
                {
                    new Dependent { DateOfBirth = new DateTime(1990, 1, 1) }
                }
            };

            Decimal result = _paycheckCalculationService.CalculateEmployeePaycheck(employee);

            Assert.Equals((1000m + (0.02m * 90000m)) * 26, result); // Base cost + additional employee cost
        }
    }
}
