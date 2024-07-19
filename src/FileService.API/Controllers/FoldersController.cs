using FileService.API.Extensions;
using FileService.Domain.Dtos.Req.Folder;
using FileService.Domain.Filters;
using FileService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class FoldersController : ControllerBase
{
    private readonly IFoldersService folderService;
    private readonly IFilesService fileService;
    private readonly IAppsService appsService;
    public FoldersController(IFoldersService folderService,IFilesService filesService, IAppsService appsService)
    {
        this.folderService = folderService;
        this.appsService = appsService;
        this.fileService = filesService;
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFolderByIdAsync(int id)
    {
        var result = await folderService.GetFolderByIdAsync(id);
        return Ok(result);
    }
    [HttpPost]
    [AuthorizeApiKey]
    public async Task<IActionResult> AddFolderAsync([FromBody] AddFolderReqDto addFolderDto)
    {
        var app = await appsService.GetAppByApiKeyAsync(HttpContext.GetApiKey());
        if(!app.Succeed)
            return Ok(app);
        var appUserId = await appsService.GetAppOwnerIdAsync(HttpContext.GetApiKey());
        var result = await folderService.AddFolderAsync(appUserId.Data!, app.Data!.Id, addFolderDto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateFolderAsync(int id, [FromBody] UpdateFolderReqDto updateFolderDto)
    {
        var result = await folderService.UpdateFolderAsync(HttpContext.GetUserId().ToString(),id, updateFolderDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteFolderAsync(int id)
    {
        var result = await folderService.DeleteFolderAsync(HttpContext.GetUserId().ToString(),id);
        return Ok(result);
    }

    [HttpGet("{id}/files")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetFilesAsync(int id)
    {
        var result = await folderService.GetFilesAsync(id);
        return Ok(result);
    }

    [HttpPost("{id}/files")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UploadFilesAsync(int id, [FromForm] List<IFormFile> files)
    {
        var result = await fileService.UploadFiles(id, files);
        return Ok(result);
    }
}
