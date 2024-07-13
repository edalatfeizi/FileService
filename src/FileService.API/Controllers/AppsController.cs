﻿using FileService.API.Extensions;
using FileService.Domain.Dtos.Req.App;
using FileService.Domain.Dtos.Req.Folder;
using FileService.Domain.Interfaces.Services;
using FileService.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppsController : ControllerBase
    {
        private readonly IAppsService appService;
        private readonly IFoldersService foldersService;
        public AppsController(IAppsService appService, IFoldersService foldersService)
        {
            this.appService = appService;
            this.foldersService = foldersService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppByIdAsync(int id)
        {
            var result = await appService.GetAppByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddAppAsync([FromBody] AddAppReqDto addAppDto)
        {
            var result = await appService.AddAppAsync(HttpContext.GetUserId().ToString(), addAppDto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppAsync(int id, [FromBody] UpdateAppReqDto updateAppDto)
        {
            var result = await appService.UpdateAppAsync(HttpContext.GetUserId().ToString(),id, updateAppDto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppAsync(int id)
        {
            var result = await appService.DeleteAppAsync(HttpContext.GetUserId().ToString(),id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppsAsync()
        {
            var result = await appService.GetAllAppsAsync();
            return Ok(result);
        }

        [HttpGet("{id}/folders")]
        public async Task<IActionResult> GetAppFolders(int id)
        {
            var result = await appService.GetFoldersAsync(id);
            return Ok(result);
        }
        [HttpPost("{id}/folder")]
        public async Task<IActionResult> AddFolderAsync(int id,[FromBody] AddFolderReqDto addFolderDto)
        {
            var result = await foldersService.AddFolderAsync(HttpContext.GetUserId().ToString(), id, addFolderDto);

            return Ok(result);
        }
    }
}
