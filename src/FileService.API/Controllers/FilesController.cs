using FileService.API.Extensions;
using FileService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class FilesController : ControllerBase
    {
        private readonly IFilesService filesService;
        public FilesController(IFilesService filesService)
        {
            this.filesService = filesService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadFileAsync(int id)
        {
            var result = await filesService.DownloadFileAsync(id);
            return File(result?.Item1, result?.Item2, result?.Item3);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileAsync(int id)
        {
            var result = await filesService.DeleteFileAsync(HttpContext.GetUserId().ToString(),id);
            return Ok(result);
        }

    }
}
