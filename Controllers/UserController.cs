using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hrm_web_api.Models.Dtos;
using hrm_web_api.Models.Entities;
using hrm_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace hrm_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;

        }

        [HttpGet]
        public async Task<ActionResult<User>> GetAllUser()
        {
            try
            {
                var user = await _userService.GetAllUserAsync();
                return Ok(user);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            try
            {
                var user = await _userService.GetUserAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (System.Exception)
            {

                throw;

            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(AddUserDto addUserDto)
        {
            try
            {
                var user = await _userService.AddUserAsync(addUserDto);
                return Ok(user);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userService.UpdateUserAsync(id, updateUserDto);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> RemoveUser(Guid id)
        {

            try
            {
                var isUser = await _userService.RemoveUserAsync(id);

                if (!isUser) return NotFound();
                return NoContent();


            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}