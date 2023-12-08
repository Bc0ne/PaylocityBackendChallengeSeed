using Api.Abstrations;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        List<Employee> employees = _employeeService.GetAllEmployees();

        // Mapping can be improved using AutoMapper
        List<GetEmployeeDto> employeeDtos = employees.Select(emp => new GetEmployeeDto
        {
            Id = emp.Id,
            FirstName = emp.FirstName,
            LastName = emp.LastName,
            DateOfBirth = emp.DateOfBirth,
            Salary = emp.Salary,
            Dependents = emp.Dependents.Select(dep => new GetDependentDto
            {
                Id = dep.Id,
                FirstName = dep.FirstName,
                LastName = dep.LastName,
                DateOfBirth = dep.DateOfBirth,
                Relationship = dep.Relationship
            }).ToList()
        }).ToList();

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employeeDtos,
            Success = true
        };

        return Ok(result);
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        ApiResponse<GetEmployeeDto> result;

        Employee employee = _employeeService.GetEmployeeById(id);

        if (employee == null)
        {
            result = new ApiResponse<GetEmployeeDto>
            {
                Success = false,
                Error = $"Employee with Id: {id} doesn't exist!"
            };

            return NotFound(result);
        }

        // Mapping can be improved using AutoMapper
        GetEmployeeDto employeeDto = new GetEmployeeDto()
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            DateOfBirth = employee.DateOfBirth,
            Salary = employee.Salary,
            Dependents = employee.Dependents.Select(dep => new GetDependentDto
            {
                Id = dep.Id,
                FirstName = dep.FirstName,
                LastName = dep.LastName,
                DateOfBirth = dep.DateOfBirth,
                Relationship = dep.Relationship
            }).ToList()
        };

        result = new ApiResponse<GetEmployeeDto>
        {
            Data = employeeDto,
            Success = true
        };

        return Ok(result);
    }

}
