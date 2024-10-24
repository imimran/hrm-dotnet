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
    public class LeaveService
    {
        private readonly ApplicationDbContext dbContext;

        public LeaveService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(List<Leave>, int)> GetAllLeaveAsync(QueryParamDto queryParams)
        {
            var query = dbContext.Leaves.AsQueryable();

            // Apply filtering by name if provided
            // if (!string.IsNullOrEmpty(queryParams.EmployeeId))
            // {
            //    query = query.Where(d => d.EmployeeId.ToLower().Contains(queryParams.EmployeeId.ToLower()));
            // }

            // Get the total count before pagination is applied
            var totalCount = await query.CountAsync();

            // Apply pagination
            var leaves = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return (leaves, totalCount);
        }

        public async Task<Leave?> GetLeaveByIdAsync(Guid id)
        {
            return await dbContext.Leaves.FindAsync(id);
        }

        public async Task<Leave> AddLeaveAsync(AddLeaveDto addLeaveDto)
        {
            var leave = new Leave
            {
                EmployeeId = addLeaveDto.EmployeeId,
                LeaveType = addLeaveDto.LeaveType,
                StartDate = addLeaveDto.StartDate,
                EndDate = addLeaveDto.EndDate,

            };
            await dbContext.Leaves.AddAsync(leave);
            await dbContext.SaveChangesAsync();
            return leave;
        }
        public async Task<Leave?> UpdateLeaveAsync(Guid id, UpdateLeaveDto updateLeaveDto)
        {
            var leave = await dbContext.Leaves.FindAsync(id);
            if (leave == null)
            {
                return null;
            }
            leave.EmployeeId = updateLeaveDto.EmployeeId;
            leave.LeaveType = updateLeaveDto.LeaveType;
            leave.StartDate = updateLeaveDto.StartDate;
            leave.EndDate = updateLeaveDto.EndDate;
            leave.Status = updateLeaveDto.Status;
            await dbContext.SaveChangesAsync();
            return leave;
        }
        public async Task<bool> RemoveLeaveAsync(Guid id)
        {
            var leave = await dbContext.Leaves.FindAsync(id);
            if (leave == null)
            {
                return false;
            }

            dbContext.Leaves.Remove(leave);
            await dbContext.SaveChangesAsync();
            return true;
        }

    }
}