using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()  ///List offers to many features
        {
            var users = await _context.Users.ToListAsync();
            return users;

        } 

        [HttpGet("{id:int}")]
        //[Authorize]
        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
    }
}