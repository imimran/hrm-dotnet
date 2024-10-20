using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using hrm_web_api.Models.Dtos;
using hrm_web_api.Models.Entities;
using hrm_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace hrm_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<Employee>> GetAllEmployees()
        {
            var employees = await employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employee = await employeeService.AddEmployeeAsync(addEmployeeDto);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateEmployee(Guid id, AddEmployeeDto addEmployeeDto)
        {
            var employee = await employeeService.UpdateEmployeeAsync(id, addEmployeeDto);
            if (employee == null)
            {
                return NotFound(id);
            }
            return Ok(employee);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> RemoveEmployee(Guid id)
        {
            var isRemoved = await employeeService.RemoveEmployeeAsync(id);
            if (!isRemoved)
            {
                return NotFound(id);
            }
            return NoContent();
        }
    }

}
