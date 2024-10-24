using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hrm_web_api.Data;
using hrm_web_api.Models.Dtos;
using hrm_web_api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hrm_web_api.Services
{
    public class PayrollService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PayrollService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<(List<Payroll>, int)> GetAllPayrollsAsync(QueryParamDto queryParams)
        {
             var query = applicationDbContext.Payrolls.AsQueryable();

            // Apply filtering by name if provided
            // if (!string.IsNullOrEmpty(queryParams.EmployeeId))
            // {
            //    query = query.Where(d => d.EmployeeId.ToLower().Contains(queryParams.EmployeeId.ToLower()));
            // }

            // Get the total count before pagination is applied
            var totalCount = await query.CountAsync();

            // Apply pagination
            var payrolls = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return (payrolls, totalCount);
        }

        public async Task<Payroll?> GetPayrollAsync(Guid id)
        {
            return await applicationDbContext.Payrolls.FindAsync(id);
        }

        public async Task<Payroll> AddPayrollAsync(AddPayrollDto addPayrollDto)
        {

            var payroll = new Payroll
            {
                EmployeeId = addPayrollDto.EmployeeId,
                PaidDate = addPayrollDto.PaidDate,
                Amount = addPayrollDto.Amount,
                Deductions = addPayrollDto.Deductions,
                PaymentMethod = addPayrollDto.PaymentMethod,
                Tax = addPayrollDto.Tax,


            };

            await applicationDbContext.Payrolls.AddAsync(payroll);
            await applicationDbContext.SaveChangesAsync();
            return payroll;


        }

        public async Task<Payroll?> UpdatePayrollAsync(Guid id, UpdatePayrollDto updatePayrollDto)
        {

            var payroll = await applicationDbContext.Payrolls.FindAsync(id);

            if (payroll == null)
            {
                return null;
            }

            payroll.Amount = updatePayrollDto.Amount;
            payroll.Deductions = updatePayrollDto.Deductions;
            payroll.PaymentMethod = updatePayrollDto.PaymentMethod;
            payroll.Tax = updatePayrollDto.Tax;
            payroll.EmployeeId = updatePayrollDto.EmployeeId;

            payroll.PaidDate = updatePayrollDto.PaidDate;

            await applicationDbContext.SaveChangesAsync();
            return payroll;
        }

        public async Task<bool> RemovePayrollAsync(Guid id)
        {
            var payroll = await applicationDbContext.Payrolls.FindAsync(id);

            if (payroll == null)
            {
                return false;
            }

            applicationDbContext.Payrolls.Remove(payroll);
            await applicationDbContext.SaveChangesAsync();
            return true;

        }
    }
}