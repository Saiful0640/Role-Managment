using FirstTimeWebAPI.DTO;
using FirstTimeWebAPI.Models;
using FirstTimeWebAPI.Services;
using FirstTimeWebAPI.Views;
using Microsoft.AspNetCore.Mvc;

namespace FirstTimeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("alluser")]
        public ActionResult<List<UserView>> AllUser()
        {
            try
            {
                List<UserView> userlist = _userService.getAllUser();
                return Ok(userlist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost("adduser")]
        public async Task<ActionResult<User>> AddUser([FromBody] UserDTO user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                await _userService.saveUser(user);
                return CreatedAtAction(nameof(AddUser), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpPut("updateuser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO user)
        {
            if (user == null || id != user.Id)
            {
                return BadRequest("User ID mismatch");
            }

            try
            {
                var result = _userService.updateUser(user);
                if (!result)
                {
                    return NotFound("User not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

       
        [HttpGet("getuserbyid/{id}")]
        public ActionResult<UserView> GetUserById(int id)
        {
            try
            {
                var user = _userService.getUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        
        // DELETE: api/UserApi/5
        [HttpDelete("deleteuser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var result = _userService.deleteUser(id);
                if (!result)
                {
                    return NotFound("User not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

       
        [HttpGet("getallrole")]
        public ActionResult<List<Role>> GetAllRole()
        {
            try
            {
                List<Role> roles1 = _userService.GetRoles();
                return Ok(roles1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getallUserType")]
        public async Task<ActionResult<List<UserType>>> GetallUserType()
        {
            try
            {
                List<UserType> userTypeList = await _userService.GetAllUserTypes();
                return Ok(userTypeList);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
