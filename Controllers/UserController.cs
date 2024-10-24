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
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserController(UserService userService, AuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                if (result == null)
                {
                    return Unauthorized("Invalid username or password.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> GetAllUser([FromQuery] QueryParamWithUsernameFilter queryParams)
        {
            try
            {
                var (users, totalCount) = await _userService.GetAllUserAsync(queryParams);

                var totalPages = (int)Math.Ceiling((double)totalCount / queryParams.PageSize);

                // Return paginated response with metadata
                return Ok(new
                {
                    TotalCount = totalCount,
                    PageSize = queryParams.PageSize,
                    CurrentPage = queryParams.Page,
                    TotalPages = totalPages,
                    Users = users
                });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            try
            {
                // Get the authenticated user's ID from claims
                var authenticatedUserId = _httpContextAccessor.HttpContext.User.FindFirst("id")?.Value;

                Console.WriteLine("authenticatedUserId" + authenticatedUserId);
                if (authenticatedUserId == null)
                {
                    return Unauthorized("User is not authenticated.");
                }

                // Check if the authenticated user's ID matches the requested ID
                if (id.ToString() != authenticatedUserId)
                {
                    return Forbid("You are not authorized to view this user's details.");
                }
                var user = await _userService.GetUserAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<User>> AddUser(AddUserDto addUserDto)
        {
            try
            {
                var user = await _userService.AddUserAsync(addUserDto);
                return Ok(user);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userService.UpdateUserAsync(id, updateUserDto);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> RemoveUser(Guid id)
        {

            try
            {
                var isUser = await _userService.RemoveUserAsync(id);

                if (!isUser) return NotFound();
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