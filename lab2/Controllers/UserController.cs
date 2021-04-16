using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lab2.Application;
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
            return View(users);
        }
        [HttpGet("{statusCode}")]
        public async Task<IActionResult> Index(Status statusCode)
        {
            ViewBag.STATUSUSERS = statusCode;
            var users = await _mediator.Send(new GetAllusers.Query());
            return View(users);
        }
        [HttpPost("")]
        public async Task<RedirectResult> CreateUser(UserDto user)
        {
            var response = await _mediator.Send(new AdduserCommand.Command(user.Name));
            return new RedirectResult($"/users/{Status.CREATED}");
        }

        [HttpPost("{id}")]
        public async Task<RedirectResult> Deleteuser(int id)
        {
           return new RedirectResult($"/users/{Status.DELETED}");
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