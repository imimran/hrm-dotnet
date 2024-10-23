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
    public class AttendanceService
    {
        private readonly ApplicationDbContext dbContext;

        public AttendanceService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(List<Attendance>, int)> GetAllAttendanceAsync(QueryParamDto queryParams)
        {
            var query = dbContext.Attendances.AsQueryable();

            // Apply filtering by name if provided
            // if (!string.IsNullOrEmpty(queryParams.EmployeeId))
            // {
            //    query = query.Where(d => d.EmployeeId.ToLower().Contains(queryParams.EmployeeId.ToLower()));
            // }

            // Get the total count before pagination is applied
            var totalCount = await query.CountAsync();

            // Apply pagination
            var attendances = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return (attendances, totalCount);
        }

        public async Task<Attendance?> GetAttendanceByIdAsync(Guid id)
        {
            return await dbContext.Attendances.FindAsync(id);
        }

        public async Task<Attendance> AddAttendanceAsync(AddAttendanceDto addAttendanceDto)
        {
            var attendance = new Attendance
            {
                EmployeeId = addAttendanceDto.EmployeeId,
                Date = addAttendanceDto.Date,
                CheckInTime = addAttendanceDto.CheckInTime,
                Status = Models.Entities.Status.Present,

            };
            await dbContext.Attendances.AddAsync(attendance);
            await dbContext.SaveChangesAsync();
            return attendance;
        }
        public async Task<Attendance?> UpdateAttendanceAsync(Guid id, UpdateAttendanceDto updateAttendanceDto)
        {
            var attendance = await dbContext.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return null;
            }
            attendance.EmployeeId = updateAttendanceDto.EmployeeId;
            attendance.Date = updateAttendanceDto.Date;
            attendance.CheckInTime = updateAttendanceDto.CheckInTime;
            attendance.CheckOutTime = updateAttendanceDto.CheckOutTime;
            attendance.Status = updateAttendanceDto.Status;
            await dbContext.SaveChangesAsync();
            return attendance;
        }
        public async Task<bool> RemoveAttendanceAsync(Guid id)
        {
            var attendance = await dbContext.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return false;
            }

            dbContext.Attendances.Remove(attendance);
            return true;
        }


    }
}