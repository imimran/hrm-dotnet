using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;
        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> GetDepartments([FromQuery] QueryParamWithNameFilter queryParams)
        {
            try
            {
                var (departments, totalCount) = await _departmentService.GetAllDepartmentAsync(queryParams);

                var totalPages = (int)Math.Ceiling((double)totalCount / queryParams.PageSize);

                // Return paginated response with metadata
                return Ok(new
                {
                    TotalCount = totalCount,
                    PageSize = queryParams.PageSize,
                    CurrentPage = queryParams.Page,
                    TotalPages = totalPages,
                    Departments = departments
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

        public async Task<ActionResult<Department>> GetDepartment(Guid id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                if (department == null) return NotFound();
                return Ok(department);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Department>> AddDepartment(AddDepartmentDto addDepartmentDto)
        {
            try
            {
                var department = await _departmentService.AddDepartmentAsync(addDepartmentDto);
                return Ok(department);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]


        public async Task<ActionResult<Department>> UpdateDepartment(Guid id, UpdateDepartmentDto updateDepartmentDto)
        {
            try
            {
                var department = await _departmentService.UpdateDepartmentAsync(id, updateDepartmentDto);
                if (department == null) return NotFound();
                return Ok(department);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                var isRemoved = await _departmentService.RemoveDepartmentAsync(id);
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