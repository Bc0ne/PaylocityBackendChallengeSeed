using Api.Models;

namespace Api.Abstrations
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();

        Employee GetEmployeeById(int employeeId);

        List<Dependent> GetAllDependents();

        Dependent GetDependentById(int dependentId);
    }
}
