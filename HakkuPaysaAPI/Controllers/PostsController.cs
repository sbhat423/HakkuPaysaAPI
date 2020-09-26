using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HakkuPaysaAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HakkuPaysaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly HPDbContext _dbContext;

        public PostsController(ILogger<PostsController> logger, HPDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            // CreatePost();
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var res = await _dbContext.Posts.ToListAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task CreatePost([FromBody] Post post)
        {
            post.Id = new Guid();
            await _dbContext.AddAsync<Post>(post);
            await _dbContext.SaveChangesAsync();
        }
    }
}
