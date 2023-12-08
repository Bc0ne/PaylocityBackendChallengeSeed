using Api.Abstrations;
using Api.Helpers;
using Api.Models;

namespace Api.Services
{
    public class PaycheckCalculationService : IPaycheckCalculationService
    {
        // Consts can be extracted into appsettings.json and mapped into configuration class
        private const decimal BaseEmployeeCostPerMonth = 1000m;
        private const decimal DependentCostPerMonth = 600m;
        private const decimal AdditionalEmployeeCostPercentage = 0.02m;
        private const decimal SalaryThresholdForAdditionalCost = 80000m;
        private const decimal AdditionalDependentCostForOlderThan50 = 200m;
        private const int DependentAgeThresholdForAdditionalCost = 50;
        private const int PaychecksPerYear = 26;

        public decimal CalculateEmployeePaycheck(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            decimal totalDeduction = BaseEmployeeCostPerMonth * PaychecksPerYear;

            if (employee.Salary > SalaryThresholdForAdditionalCost)
            {
                totalDeduction += AdditionalEmployeeCostPercentage * employee.Salary;
            }

            foreach (Dependent dependent in employee.Dependents)
            {
                totalDeduction += CalculateDependentCost(dependent);
            }

            return totalDeduction / PaychecksPerYear;
        }

        private decimal CalculateDependentCost(Dependent dependent)
        {
            decimal dependentCost = DependentCostPerMonth * PaychecksPerYear;

            if (AgeHelper.CalculateAge(dependent.DateOfBirth) > DependentAgeThresholdForAdditionalCost)
            {
                dependentCost += AdditionalDependentCostForOlderThan50 * PaychecksPerYear;
            }

            return dependentCost;
        }
    }
}
