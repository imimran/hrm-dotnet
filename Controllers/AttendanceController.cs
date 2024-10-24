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
    public class AttendanceController : ControllerBase
    {
        private readonly AttendanceService _attendanceService;
        public AttendanceController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> GetAttendances([FromQuery] QueryParamDto queryParams)
        {
            try
            {
                var (attendances, totalCount) = await _attendanceService.GetAllAttendanceAsync(queryParams);

                var totalPages = (int)Math.Ceiling((double)totalCount / queryParams.PageSize);

                // Return paginated response with metadata
                return Ok(new
                {
                    TotalCount = totalCount,
                    PageSize = queryParams.PageSize,
                    CurrentPage = queryParams.Page,
                    TotalPages = totalPages,
                    Attendances = attendances
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

        public async Task<ActionResult<Attendance>> GetAttendance(Guid id)
        {
            try
            {
                var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
                if (attendance == null) return NotFound();
                return Ok(attendance);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Attendance>> AddAttendance(AddAttendanceDto addAttendanceDto)
        {
            try
            {
                var attendance = await _attendanceService.AddAttendanceAsync(addAttendanceDto);
                return Ok(attendance);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]


        public async Task<ActionResult<Attendance>> UpdateAttendance(Guid id, UpdateAttendanceDto updateAttendanceDto)
        {
            try
            {
                var attendance = await _attendanceService.UpdateAttendanceAsync(id, updateAttendanceDto);
                if (attendance == null) return NotFound();
                return Ok(attendance);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> DeleteAttendance(Guid id)
        {
            try
            {
                var isRemoved = await _attendanceService.RemoveAttendanceAsync(id);
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