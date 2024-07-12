using FileService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileAsync(int id)
        {
            var result = await filesService.DeleteFileAsync(id);
            return Ok(result);
        }

    }
}
