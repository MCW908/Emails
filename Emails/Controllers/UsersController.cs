using Emails.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUsersService _usersService;

        public UsersController(ILogger<UsersController> logger, IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        // GET: Users
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<UsersDTO> users = null;
            try
            {
                users = await _usersService.GetUsersAsync(email);
            }
            catch
            {
                _logger.LogWarning("Exception occurred using Users service.");
                users = Array.Empty<UsersDTO>();
            }
            //return View(users.ToList());
            return CreatedAtAction(nameof(Index), users.ToList());
        }

        // GET: Users/4
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var user = await _usersService.GetUserAsync(id.Value);
                if (user == null)
                {
                    return NotFound();
                }
                return CreatedAtAction(nameof(Details), user);
            }
            catch
            {
                _logger.LogWarning("Exception occurred using Users service.");
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

    }
}
