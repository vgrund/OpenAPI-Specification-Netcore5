using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Users.DTOs;

namespace Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsersController : ControllerBase
    {
        private static readonly string[] Names = new[]
        {
            "John", "Paul", "Ringo", "George", "Pete", "Stuart", "Norman", "Chas", "Tommy", "Jimmy"
        };

        private static readonly string[] LastNames = new[]
        {
            "Lennon", "McCartney", "Starr", "Harrison", "Best", "Sutcliffe", "Chapman", "Newby", "Moore", "Nicol"
        };

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(
            OperationId = "GetAll_Users",
            Summary = "Obtêm todos os users",
            Description = "Este recurso lista todos os users cadastrados",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "The list of users", typeof(IEnumerable<User>))]
        public IEnumerable<User> GetAll()
        {
            var rng = new Random();
            var nextIndex = getRandomNext();
            return Enumerable.Range(1, 5).Select(index =>
            {
                return new User
                {
                    FirstName = Names[nextIndex],
                    LastName = LastNames[nextIndex],
                    Email = $"{Names[nextIndex] + LastNames[nextIndex]}@gmail.com",
                    Id = Guid.NewGuid(),
                    Phone = rng.Next(-20, 1000) + ""
                };
            })
            .ToArray();
        }

        [HttpGet("{userId}")]
        [SwaggerOperation(
            OperationId = "GetById_Users",
            Summary = "Obtêm um user pelo seu Id",
            Description = "Este recurso apresenta o user",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "An user", typeof(User))]
        public User GetById(Guid userId)
        {
            var rng = new Random();
            var nextIndex = getRandomNext();

            return new User
            {
                FirstName = Names[nextIndex],
                LastName = LastNames[nextIndex],
                Email = $"{Names[nextIndex] + LastNames[nextIndex]}@gmail.com",
                Id = userId,
                Phone = rng.Next(-20, 1000) + ""
            };
        }

        [HttpPost()]
        [SwaggerOperation(
            OperationId = "Create_Users",
            Summary = "Cria um user",
            Description = "Este recurso cria um user",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(StatusCodes.Status201Created, "An user", typeof(User))]
        public IActionResult Post([FromBody] User user)
        {
            return Created("", user);
        }

        private int getRandomNext()
        {
            var rng = new Random();
            return rng.Next(Names.Length);
        }
    }
}
