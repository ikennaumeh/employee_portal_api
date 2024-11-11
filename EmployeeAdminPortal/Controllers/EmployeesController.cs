using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Dto;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    public EmployeesController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAllEmployees()
    {
        var allEmployees = dbContext.Employees.ToList();
        return Ok(allEmployees);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetEmployeeById([FromRoute] Guid id)
    {
        //var employee = dbContext.Employees.Find(id);

        var employee = dbContext.Employees.FirstOrDefault(x => x.Id == id);

        if(employee is null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPost]
    public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
    {
        var employee = new Employee()
        {
            Name = addEmployeeDto.Name,
            Email = addEmployeeDto.Email,
            Phone = addEmployeeDto.Phone,
            Salary = addEmployeeDto.Salary,
        };

        dbContext.Employees.Add(employee);
        dbContext.SaveChanges();

        return Ok(employee);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
    {
        var employee = dbContext.Employees.Find(id);

        if(employee is null)
        {
            return NotFound();
        }

        employee.Name = updateEmployeeDto.Name;
        employee.Email = updateEmployeeDto.Email;
        employee.Phone = updateEmployeeDto.Phone;
        employee.Salary = updateEmployeeDto.Salary;

        dbContext.SaveChanges();

        return Ok(employee);
    }


    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteEmployee(Guid id)
    {
        var employee = dbContext.Employees.Find(id);

        if(employee is null)
        {
            return NotFound();
        }

        dbContext.Employees.Remove(employee);
        dbContext.SaveChanges();
        return Ok();
    }
}
