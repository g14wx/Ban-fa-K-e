using System.Collections.Generic;
using System.Threading.Tasks;
using lab2.Application.Commands.User;
using lab2.Application.Queries.User;
using lab2.Domain.DTOs;
using lab2.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lab2.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private IMediator _mediator;
        private ILogger _logger;
        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var users = await _mediator.Send(new GetAllusers.Query());
            _logger.LogInformation(users[0].Name);
            return View(users);
        }

        [HttpPost("{id}")]
        public IActionResult Deleteuser(int id)
        {
           return Ok("");
        }

        [HttpPost("api")]
        public async Task<IActionResult> CreateUserApi( [FromBody]  UserDto user )
        {
            var response = await _mediator.Send(new AdduserCommand.Command(user.Name));
            return Ok(response);
        }

        [HttpGet("api/{id}")]
        public async Task<IActionResult> GetUserByIdApi(int id)
        {
            var response = _mediator.Send(new GetuserById.Query(id));

            return response == null ? NotFound() : Ok(response);
        }
    }
}