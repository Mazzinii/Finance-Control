﻿using Microsoft.AspNetCore.Mvc;
using Person.Data;
using Person.Models.Requests;
using PersonTransation.Models.Entities;
using PersonTransation.Models.Requests;
using PersonTransation.Services;

namespace PersonTransation.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly PersonTransationContext _context = new PersonTransationContext();

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpPost("user")]
        public async Task<IResult> CreateUser(UserRequest req)
        {
            var person = new UserModel(req.Name, req.Email, req.Password);

            return await _service.Create(person, _context);
        }

        [HttpPost("user/login")]
        public async Task<IResult> LoginUser(LoginHashRequests.LoginRequest req, LoginHashRequests login)
        {
            var person = await login.Handle(req);
            return Results.Ok(person);
        }

        [HttpGet("user/{page}/{limit}")]
        public async Task<IResult> GetUserPagination(int page, int limit)
        {
            return await _service.Get(_context, page, limit);
        }

        [HttpPatch("user/{id}")]
        public async Task<IResult> PatchUser(Guid id, UserRequest req)
        {

            var person = new UserModel(req.Name, req.Email, req.Password);

            return await _service.Patch(person, _context, id);
        }

        [HttpDelete("user/{id}")]
        public async Task<IResult> DeleteUser(Guid id)
        {
            return await _service.Delete(_context, id);
        }
    }
}
