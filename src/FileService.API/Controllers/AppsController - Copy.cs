﻿using FileService.API.Extensions;
using FileService.Domain.Dtos.Req.Folder;
using FileService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly IFoldersService folderService;
        public FoldersController(IFoldersService folderService)
        {
            this.folderService = folderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFolderByIdAsync(int id)
        {
            var result = await folderService.GetFolderByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddFolderAsync([FromBody] AddFolderReqDto addFolderDto)
        {
            var result = await folderService.AddFolderAsync(HttpContext.GetApiKey(), addFolderDto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFolderAsync(int id, [FromBody] UpdateFolderReqDto updateFolderDto)
        {
            var result = await folderService.UpdateFolderAsync(id, updateFolderDto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolderAsync(int id)
        {
            var result = await folderService.DeleteFolderAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}/files")]
        public async Task<IActionResult> GetFilesAsync(int id)
        {
            var result = await folderService.GetFilesAsync(id);
            return Ok(result);
        }

    }
}