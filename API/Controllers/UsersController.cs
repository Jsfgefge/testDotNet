using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext context;

        public UsersController(DataContext _context)
        {
            this.context = _context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await this.context.Users.ToListAsync();
            return users;
        }
        [Authorize]
        [HttpGet("{id}")] // api/users/{id}
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await this.context.Users.FindAsync(id);
            if (user is null) return NotFound();
            return user;
        }
    }

}
