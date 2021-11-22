using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using serverApp.Models;
using serverApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using serverApp.DTO;
using serverApp.Helpers;

namespace serverApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _context;
        private readonly JwtService _jwtService;

        public UserController(IUserRepository context,JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserModel model)
        {
            var newModel = new UserModel()
            {
                Email = model.Email,
                Name = model.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                UserType = model.UserType
            };
            var response = await _context.RegisterAsync(newModel);
            return Created("Success", response);
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _context.GetAllUsers();
            return Ok(response);
        }
        

        [HttpGet("getuser")] // hits by frontend everytime for authorization
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var response = await _context.GetUserAsync(userId);

                if (response == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(response);
                }

            }catch(Exception e)
            {
                return Unauthorized();
            }

        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute]int id , [FromBody]UserModel model)
        {
            if (ModelState.IsValid)
            {

                var response = await _context.UpdateUserAsync(id, model);
                if (response > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new { response });
                }
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpDelete("rm/{id}")]
        public async Task<IActionResult> RemoveUser([FromRoute] int id)
        {
            var response = await _context.RemoveUserAsync(id);
            if (response > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var response = await _context.GetByEmail(model.Email);
            if (response == null) return BadRequest(new { message = "Invalid EmailId" });
            if(!BCrypt.Net.BCrypt.Verify(model.Password , response.Password))
            {
                return BadRequest(new { message = "Invalid Password" });
            }
            
            var jwt = _jwtService.Generate(response.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(new { 
            message="success"
            });   // frontend if response.UserType check .
        }
    }
}
