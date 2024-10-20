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

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await dbContext.Employees.ToListAsync();
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
