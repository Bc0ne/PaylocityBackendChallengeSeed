using Api.Abstrations;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaychecksController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPaycheckCalculationService _paycheckCalculationService;

        public PaychecksController(IEmployeeService employeeService, IPaycheckCalculationService paycheckCalculationService)
        {
            _employeeService = employeeService;
            _paycheckCalculationService = paycheckCalculationService;
        }

        [SwaggerOperation(Summary = "Calculate Employee Paycheck by employee id")]
        [HttpGet("calculate/{employeeId}")]
        public async Task<ActionResult<ApiResponse<CalculateEmployeePaycheckDto>>> CalcalatePaycheckByEmployeeId(int employeeId)
        {
            ApiResponse<CalculateEmployeePaycheckDto> result;

            Employee employee = _employeeService.GetEmployeeById(employeeId);

            if (employee == null)
            {
                result = new ApiResponse<CalculateEmployeePaycheckDto>
                {
                    Success = false,
                    Error = $"Employee with Id: {employeeId} doesn't exist!"
                };

                return NotFound(result);
            }

            decimal totalDeductions = _paycheckCalculationService.CalculateEmployeePaycheck(employee);

            CalculateEmployeePaycheckDto employeePaycheckDto = new CalculateEmployeePaycheckDto
            {
                EmployeeId = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                TotalDeductions = Math.Round(totalDeductions, 2),
                SalaryAfterDeductions = Math.Round(employee.Salary - totalDeductions, 2)
            };

            result = new ApiResponse<CalculateEmployeePaycheckDto>
            {
                Data = employeePaycheckDto,
                Success = true
            };

            return Ok(result);
        }
    }
}
