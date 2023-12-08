using Api.Models;

namespace Api.Abstrations
{
    public interface IPaycheckCalculationService
    {
        decimal CalculateEmployeePaycheck(Employee employee);
    }
}
