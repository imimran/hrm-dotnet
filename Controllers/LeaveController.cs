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
    public class LeaveController : ControllerBase
    {
        private readonly LeaveService _leaveService;
        public LeaveController(LeaveService leaveService)
        {
            _leaveService = leaveService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> GetLeaves([FromQuery] QueryParamDto queryParams)
        {
            try
            {
                var (leaves, totalCount) = await _leaveService.GetAllLeaveAsync(queryParams);

                var totalPages = (int)Math.Ceiling((double)totalCount / queryParams.PageSize);

                // Return paginated response with metadata
                return Ok(new
                {
                    TotalCount = totalCount,
                    PageSize = queryParams.PageSize,
                    CurrentPage = queryParams.Page,
                    TotalPages = totalPages,
                    Leaves = leaves
                });

            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Leave>> GetLeave(Guid id)
        {
            try
            {
                var leave = await _leaveService.GetLeaveByIdAsync(id);
                if (leave == null) return NotFound();
                return Ok(leave);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
         [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Leave>> AddLeave(AddLeaveDto addLeaveDto)
        {
            try
            {
                var leave = await _leaveService.AddLeaveAsync(addLeaveDto);
                return Ok(leave);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id:guid}")]
         [Authorize(Roles = "Admin")]


        public async Task<ActionResult<Leave>> UpdateLeave(Guid id, UpdateLeaveDto updateLeaveDto)
        {
            try
            {
                var leave = await _leaveService.UpdateLeaveAsync(id, updateLeaveDto);
                if (leave == null) return NotFound();
                return Ok(leave);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
         [Authorize(Roles = "Admin")]

        public async Task<ActionResult> DeleteLeave(Guid id)
        {
            try
            {
                var isRemoved = await _leaveService.RemoveLeaveAsync(id);
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