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
    public class DepartmentService
    {
        private readonly ApplicationDbContext dbContext;

        public DepartmentService(ApplicationDbContext dbContext)

        {
            this.dbContext = dbContext;
        }

        public async Task<(List<Department>, int)> GetAllDepartmentAsync(QueryParamWithNameFilter queryParams)
        {
            var query = dbContext.Departments.AsQueryable();

            // Apply filtering by name if provided
            if (!string.IsNullOrEmpty(queryParams.Name))
            {
               query = query.Where(d => d.Name.ToLower().Contains(queryParams.Name.ToLower()));
            }

            // Get the total count before pagination is applied
            var totalCount = await query.CountAsync();

            // Apply pagination
            var departments = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return (departments, totalCount);
        }

        public async Task<Department?> GetDepartmentByIdAsync(Guid id)
        {
            return await dbContext.Departments.FindAsync(id);
        }

        public async Task<Department> AddDepartmentAsync(AddDepartmentDto addDepartmentDto)
        {
            var department = new Department
            {
                Name = addDepartmentDto.Name,
                Description = addDepartmentDto.Description,
                ManagerId = addDepartmentDto.ManagerId,
            };
            await dbContext.Departments.AddAsync(department);
            await dbContext.SaveChangesAsync();
            return department;
        }
        public async Task<Department?> UpdateDepartmentAsync(Guid id, UpdateDepartmentDto updateDepartmentDto)
        {
            var department = await dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return null;
            }
            department.Name = updateDepartmentDto.Name;
            department.Description = updateDepartmentDto.Description;
            department.ManagerId = updateDepartmentDto.ManagerId;

            await dbContext.SaveChangesAsync();
            return department;
        }
        public async Task<bool> RemoveDepartmentAsync(Guid id)
        {
            var department = await dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return false;
            }

            dbContext.Departments.Remove(department);
            return true;
        }
    }
}