using Api.Abstrations;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public DependentsController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        List<Dependent> dependents = _employeeService.GetAllDependents();

        // Mapping can be improved using AutoMapper
        List<GetDependentDto> dependentDtos = dependents.Select(dep => new GetDependentDto
        {
            Id = dep.Id,
            FirstName = dep.FirstName,
            LastName = dep.LastName,
            DateOfBirth = dep.DateOfBirth,
            Relationship = dep.Relationship
        }).ToList();

        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = dependentDtos,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        ApiResponse<GetDependentDto> result;

        Dependent dependent = _employeeService.GetDependentById(id);

        if (dependent == null)
        {
            result = new ApiResponse<GetDependentDto>
            {
                Success = false,
                Error = $"Dependent with Id: {id} doesn't exist!"
            };

            return NotFound(result);
        }

        // Mapping can be improved using AutoMapper
        GetDependentDto dependentDto = new GetDependentDto()
        {
            Id = dependent.Id,
            FirstName = dependent.FirstName,
            LastName = dependent.LastName,
            DateOfBirth = dependent.DateOfBirth,
            Relationship = dependent.Relationship
        };

        result = new ApiResponse<GetDependentDto>
        {
            Data = dependentDto,
            Success = true
        };

        return Ok(result);
    }
}
