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
    public class UserService
    {
        private readonly ApplicationDbContext dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(List<User>, int)> GetAllUserAsync(QueryParamWithUsernameFilter queryParams)
        {
            var query = dbContext.Users.AsQueryable();

            // Apply filtering by name if provided
            if (!string.IsNullOrEmpty(queryParams.Username))
            {
                query = query.Where(d => d.Username.ToLower().Contains(queryParams.Username.ToLower()));
            }
            // Get the total count before pagination is applied
            var totalCount = await query.CountAsync();

            // Apply pagination
            var users = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return (users, totalCount);

        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            return await dbContext.Users.FindAsync(id);


        }

        public async Task<User> AddUserAsync(AddUserDto addUserDto)
        {
            var user = new User
            {
                Username = addUserDto.Username,
                Role = addUserDto.Role,
                Password = BCrypt.Net.BCrypt.HashPassword(addUserDto.Password),

            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;

        }

        public async Task<User?> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null) return null;
            user.Username = updateUserDto.Username;
            user.Role = updateUserDto.Role;
            user.Password = updateUserDto.Password;
            user.EmployeeId = updateUserDto.EmployeeId;
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> RemoveUserAsync(Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null) return false;

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return true;

        }
    }
}