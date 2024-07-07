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
    }
}
