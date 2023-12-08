using Api.Models;

namespace Api.Abstrations
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();

        Employee GetEmployeeById(int employeeId);

        List<Dependent> GetAllDependents();

        Dependent GetDependentById(int dependentId);
    }
}
