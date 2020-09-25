using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HakkuPaysaAPI.Entities;
using Microsoft.AspNetCore.Mvc;
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
            var res = _dbContext.Posts.ToList();
            return Ok(res);
        }

        [HttpPost]
        public async Task CreatePost()
        {
            await _dbContext.AddAsync<Post>(new Post()
            {
                Id = new Guid(),
                Title = "asfdsafd",
                Summary = "sadfdfa"
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}
