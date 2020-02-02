using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Entities;
using Entities.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthen.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class UsersController : ControllerBase
    {
        protected readonly IUsersBusiness _bizUsers;
        public UsersController(IUsersBusiness usersBusiness)
        {
            _bizUsers = usersBusiness;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var result = _bizUsers.Authenticate(model.Username, model.Password);
            if(result == null)
            {
                return BadRequest(new { message = "Not found user" });
            }
            return Ok(result);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var result = _bizUsers.GetAll();
            return Ok(result);
        }
        [Authorize(Roles = "Author,Reader")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _bizUsers.GetById(id);
            if(result==null)
            {
                return BadRequest(new { message = "Not found user" });
            }
            return Ok(result);
        }
    }
}