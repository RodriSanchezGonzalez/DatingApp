using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository repo;
        private readonly IMapper mapper;
        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await this.repo.GetUsers();
            var usersToReturn = this.mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToReturn);
        }

        private int IEnumerable<T>(IEnumerable<models.User> users)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await this.repo.GetUser(id);

            var userToReturn = this.mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFormRepo = await repo.GetUser(id);
            this.mapper.Map(userForUpdateDto, userFormRepo);

            if (await this.repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");

        }
    }
}