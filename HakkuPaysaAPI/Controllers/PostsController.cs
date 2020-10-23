using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HakkuPaysaAPI.Entities;
using HakkuPaysaAPI.Services.FileStorage;
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
        private readonly IFileStorageService _fileStorageService;

        public PostsController(ILogger<PostsController> logger, HPDbContext dbContext, IFileStorageService fileStorageService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _fileStorageService = fileStorageService;
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
            if (!string.IsNullOrWhiteSpace(post.Pic)) 
            {
                try
                {
                    var path = await _fileStorageService.SaveFile(Convert.FromBase64String(post.Pic), "png", "posts");
                    post.Pic = path;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
            await _dbContext.AddAsync<Post>(post);
            await _dbContext.SaveChangesAsync();
        }
    }
}
