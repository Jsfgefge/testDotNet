using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API
{
    [ApiController]
    [Route("api/[controller]")] // api/users
    public class UsersController : ControllerBase
    {
        private readonly DataContext context;

        public UsersController(DataContext _context)
        {
            this.context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await this.context.Users.ToListAsync();
            return users;
        }

        [HttpGet("{id}")] // api/users/{id}
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await this.context.Users.FindAsync(id);
            if (user is null) return NotFound();
            return user;
        }
    }

}
