using FileService.Domain.Dtos.Req.App;
using FileService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppsController : ControllerBase
    {
        private readonly IAppService appService;
        public AppsController(IAppService appService)
        {
            this.appService = appService;
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
            var result = await appService.AddAppAsync(addAppDto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppAsync(int id, [FromBody] UpdateAppReqDto updateAppDto)
        {
            var result = await appService.UpdateAppAsync(id, updateAppDto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppAsync(int id)
        {
            var result = await appService.DeleteAppAsync(id);
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
    }
}
