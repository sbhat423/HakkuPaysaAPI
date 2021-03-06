﻿using HakkuPaysaAPI.DTOs.UserProfileDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserProfileController: ControllerBase
    {
        private readonly ILogger<UserProfileController> _logger;
        private readonly HakkuPayasaDbContext _dbContext;

        public UserProfileController(ILogger<UserProfileController> logger, HakkuPayasaDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("{Username}")]
        public async Task<IActionResult> GetUserProfile([FromRoute] string Username)
        {
            // Username is separate from userId but unique. UserId is not used as I need to find a way to get it from the Indentity to PWA
            var userProfile = await _dbContext.Users.FirstOrDefaultAsync((user) => user.Username == Username);
            return Ok(userProfile.ToDto());
        }

        [HttpPut]
        [Route("{Username}")]
        public async Task<IActionResult> GetUserProfile([FromBody] UserProfileForUpdateDto profileForUpdate,[FromRoute] string Username)
        {
            var existingProfile = await _dbContext.Users.FirstOrDefaultAsync((user) => user.Username == Username);

            existingProfile.FirstName = profileForUpdate.FirstName;
            existingProfile.LastName = profileForUpdate.LastName;
            existingProfile.ProfilePic = profileForUpdate.ProfilePic;
            existingProfile.Status = profileForUpdate.Status;
            existingProfile.DOB = profileForUpdate.DOB;
            existingProfile.Intro = profileForUpdate.Intro;

            _dbContext.Users.Update(existingProfile);
            await _dbContext.SaveChangesAsync();

            return Ok(existingProfile);
        }
    }
}
