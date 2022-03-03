using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDScanBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok("File Upload API running...");
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult Upload(IFormFile file, string? id)
        {

            return Ok();
        }

    }

}
