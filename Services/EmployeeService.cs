using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using hrm_web_api.Data;
using hrm_web_api.Models.Dtos;
using hrm_web_api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hrm_web_api.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(List<Employee>, int)> GetAllEmployeesAsync(QueryParamWithNameFilter queryParams)
        {
            // return await dbContext.Employees.ToListAsync();
            var query = dbContext.Employees.AsQueryable();

            // Apply filtering by name if provided
            if (!string.IsNullOrEmpty(queryParams.Name))
            {
                query = query.Where(d => d.Name.ToLower().Contains(queryParams.Name.ToLower()));
            }

            // Get the total count before pagination is applied
            var totalCount = await query.CountAsync();

            // Apply pagination
            var employees = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return (employees, totalCount);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
        {
            return await dbContext.Employees.FindAsync(id);
        }

        public async Task<Employee> AddEmployeeAsync(AddEmployeeDto addEmployeeDto)
        {
            var employee = new Employee
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
                Address = addEmployeeDto.Address,
                BirthDate = addEmployeeDto.BirthDate,
                JoinDate = addEmployeeDto.JoinDate,
                ManagerId = addEmployeeDto.ManagerId,
                DepartmentId = addEmployeeDto.DepartmentId,
            };

            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> UpdateEmployeeAsync(Guid id, AddEmployeeDto addEmployeeDto)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return null;
            }

            employee.Name = addEmployeeDto.Name;
            employee.Email = addEmployeeDto.Email;
            employee.Phone = addEmployeeDto.Phone;
            employee.Salary = addEmployeeDto.Salary;
            employee.Address = addEmployeeDto.Address;
            employee.BirthDate = addEmployeeDto.BirthDate;
            employee.JoinDate = addEmployeeDto.JoinDate;
            employee.ManagerId = addEmployeeDto.ManagerId;
            employee.DepartmentId = addEmployeeDto.DepartmentId;

            await dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> RemoveEmployeeAsync(Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }

            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
