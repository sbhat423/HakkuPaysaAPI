using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HakkuPaysaAPI.DTOs;
using HakkuPaysaAPI.DTOs.PaginationDto;
using HakkuPaysaAPI.DTOs.Post;
using HakkuPaysaAPI.Entities;
using HakkuPaysaAPI.Services.FileStorage;
using HakkuPaysaAPI.Utilities;
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
        private readonly HakkuPayasaDbContext _dbContext;
        private readonly IFileStorageService _fileStorageService;

        public PostsController(ILogger<PostsController> logger, HakkuPayasaDbContext dbContext, IFileStorageService fileStorageService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var res = await _dbContext.Posts.Select((post) => post.ToDto()).ToListAsync();
            return Ok(res);
        }

        [HttpGet]
        [Route("mine/{Username}")]
        public async Task<IActionResult> GetMyPosts([FromRoute] string Username)
        {
            var res = await _dbContext.Posts.Where(p => p.Author.Username == Username).Select((post) => post.ToDto()).ToListAsync();
            return Ok(res);
        }

        [HttpPost]
        [Route("paginated")]
        public async Task<IActionResult> GetPosts([FromBody] PaginationOptionDto paginationOption)
        {
            var paginator = new Paginator(paginationOption);
            var res = await paginator.GetPagedResult(_dbContext.Posts);
            return Ok(res);
        }

        [HttpPost]
        public async Task CreatePost([FromBody] PostForCreateDto postForCreateDto)
        {
            var post = new Post();
            post.CreatedOn = postForCreateDto.CreatedOn;
            post.UpdatedOn = postForCreateDto.CreatedOn;
            post.Title = postForCreateDto.Title;
            post.Summary = postForCreateDto.Summary;
            post.Likes = 0;
            post.Comments = new List<Comment>();
            post.Author = postForCreateDto.Author;

            if (!string.IsNullOrWhiteSpace(postForCreateDto.Pic))
            {
                try
                {
                    var path = await _fileStorageService.SaveFile(Convert.FromBase64String(postForCreateDto.Pic), "png", "posts");
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

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetPosts([FromRoute] string Id)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == Id);
            if (post == null)
            {
                return BadRequest($"Post does not exist for the Id {Id}");
            }
            return Ok(post.ToDto());
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> UpdatePost([FromBody] PostForUpdateDto Post, [FromRoute] string Id)
        {
            var existingPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingPost == null)
            {
                return BadRequest($"Post does not exist for the Id {Post.Id}");
            }
            existingPost.UpdatedOn = Post.UpdatedOn;
            existingPost.Pic = Post.Pic;
            existingPost.Summary = Post.Summary;
            existingPost.Title = Post.Title;
            existingPost.Likes = Post.Likes;
            // remove it after adding the Comments controller
            existingPost.Comments = Post.Comments;

            _dbContext.Posts.Update(existingPost);
            await _dbContext.SaveChangesAsync();

            return Ok(existingPost);
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeletePost([FromRoute] string Id)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync<Post>(p => p.Id == Id);

            if (post == null)
            {
                return BadRequest($"Post with Id {Id} not found");
            }
            else 
            {
                var res = _dbContext.Remove<Post>(post);
                if (res != null)
                {
                    await _dbContext.SaveChangesAsync();
                    return Ok();
                }
                else 
                {
                    return BadRequest("Failed to delete the provided post");
                }
            }

        }
    }
}
