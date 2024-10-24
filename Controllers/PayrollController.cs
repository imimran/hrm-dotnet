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
    public class PayrollController : ControllerBase
    {
        private readonly PayrollService _payrollService;
        public PayrollController(PayrollService payrollService)
        {
            _payrollService = payrollService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> GetPayrolls([FromQuery] QueryParamDto queryParams)
        {
            try
            {
                var (payrolls, totalCount) = await _payrollService.GetAllPayrollsAsync(queryParams);

                var totalPages = (int)Math.Ceiling((double)totalCount / queryParams.PageSize);

                // Return paginated response with metadata
                return Ok(new
                {
                    TotalCount = totalCount,
                    PageSize = queryParams.PageSize,
                    CurrentPage = queryParams.Page,
                    TotalPages = totalPages,
                    Payrolls = payrolls
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
        public async Task<ActionResult<Payroll>> GetPayroll(Guid id)
        {
            try
            {
                var payroll = await _payrollService.GetPayrollAsync(id);
                if (payroll == null) return NotFound();
                return Ok(payroll);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Payroll>> AddPayroll(AddPayrollDto addPayrollDto)
        {
            try
            {
                var payroll = await _payrollService.AddPayrollAsync(addPayrollDto);
                return Ok(payroll);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]


        public async Task<ActionResult<Payroll>> UpdatePayroll(Guid id, UpdatePayrollDto updatePayrollDto)
        {
            try
            {
                var payroll = await _payrollService.UpdatePayrollAsync(id, updatePayrollDto);
                if (payroll == null) return NotFound();
                return Ok(payroll);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> DeletePayroll(Guid id)
        {
            try
            {
                var isRemoved = await _payrollService.RemovePayrollAsync(id);
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