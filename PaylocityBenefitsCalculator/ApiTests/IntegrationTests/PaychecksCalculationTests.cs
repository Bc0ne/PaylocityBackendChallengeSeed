using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Dtos.Employee;
using Xunit;

namespace ApiTests.IntegrationTests
{
    public class PaychecksCalculationTests : IntegrationTest
    {
        public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
        {
            var response = await HttpClient.GetAsync("/api/v1/employees/1");
            var employee = new GetEmployeeDto
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            };
            await response.ShouldReturn(HttpStatusCode.OK, employee);
        }

        [Fact]
        public async Task CalculateTotalDeduction_EmployeeBelowThreshold_NoAdditionalCost()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("/api/v1/paychecks/calculate/1");
            CalculateEmployeePaycheckDto employeePaycheck = new CalculateEmployeePaycheckDto
            {
                EmployeeId = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                TotalDeductions = 1000m,
                SalaryAfterDeductions = 74420.99m
            };

            await response.ShouldReturn(HttpStatusCode.OK, employeePaycheck);
        }
    }
}
