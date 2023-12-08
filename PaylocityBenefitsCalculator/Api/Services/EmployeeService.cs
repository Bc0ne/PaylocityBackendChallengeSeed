using Api.Abstrations;
using Api.Models;

namespace Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeRepository.GetEmployeeById(employeeId);
        }

        // Should be extracted to Dependents service
        public List<Dependent> GetAllDependents()
        {
            return _employeeRepository.GetAllDependents();
        }

        // Should be extracted to Dependents service
        public Dependent GetDependentById(int dependentId)
        {
            return _employeeRepository.GetDependentById(dependentId);
        }
    }
}
