using AppointmentSchedSys.Models;
using AppointmentSchedSys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class UserController : ControllerBase
    {
        private readonly ILogin _loginService;
        private ILogger<UserController> _logger;
        private readonly IRepo<string, User> _repo;

        public UserController(ILogin loginService, ILogger<UserController> logger, IRepo<string, User> repo)
        {
            _loginService = loginService;
            _logger = logger;
            _repo = repo;
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(UserDTO userDTO)
        {
            var result = _loginService.Login(userDTO);
            if (result != null)
            {
                _logger.LogInformation("Login");
                return Ok(result);
            }
            return BadRequest("Invalid username or password");
        }
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(User user)
        {
            user.Role = "User";
            var result = _loginService.Register(user);
            if (result != null)
                return Ok(result);
            return BadRequest("Could not register user");
        }

        [HttpGet]
        [Route("GetUserId")]
        public ActionResult GetUserID (String username)
        {
            User u = _repo.Get(username);
            if (u == null)
                return Ok(u);
            return Ok(u.Id);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUserDetails")]
        public ActionResult GetUserDetails(String username)
        {
            User u = _repo.Get(username);
            return Ok(u);
        }

        [Authorize]
        [HttpPut]
        [Route("update")]
        public ActionResult UpdateUser(User user)
        {
            var result = _repo.Update(user);
            if (result != null)
                return Ok(result);
            return BadRequest("Could not update user");
        }
    }
}
