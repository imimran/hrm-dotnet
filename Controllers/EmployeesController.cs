using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using hrm_web_api.Models.Dtos;
using hrm_web_api.Models.Entities;
using hrm_web_api.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Employee>> GetAllEmployees([FromQuery] QueryParamWithNameFilter queryParams)
        {
            try
            {
                var (employees, totalCount) = await employeeService.GetAllEmployeesAsync(queryParams);

                var totalPages = (int)Math.Ceiling((double)totalCount / queryParams.PageSize);

                // Return paginated response with metadata
                return Ok(new
                {
                    TotalCount = totalCount,
                    PageSize = queryParams.PageSize,
                    CurrentPage = queryParams.Page,
                    TotalPages = totalPages,
                    Employees = employees
                });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            try
            {
                var employee = await employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Employee>> AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            try
            {
                var employee = await employeeService.AddEmployeeAsync(addEmployeeDto);
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> UpdateEmployee(Guid id, AddEmployeeDto addEmployeeDto)
        {
            try
            {
                var employee = await employeeService.UpdateEmployeeAsync(id, addEmployeeDto);
                if (employee == null)
                {
                    return NotFound(id);
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> RemoveEmployee(Guid id)
        {
            try
            {
                var isRemoved = await employeeService.RemoveEmployeeAsync(id);
                if (!isRemoved)
                {
                    return NotFound(id);
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }

}
