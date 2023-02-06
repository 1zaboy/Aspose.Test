using System.ComponentModel.DataAnnotations;
using Aspose.Core.Apis;
using Aspose.Core.Models;
using Aspose.Core.Services;
using Aspose.Core.Utils;
using Aspose.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aspose.Controllers;

public class UploadModel
{
    [Required]
    public IFormFile Image { get; set; }
}

[Route("api/[controller]")]
public class ImageController : Controller
{
    private IImageService m_ImageService;

    public ImageController(IImageService imageService)
    {
        m_ImageService = imageService;
    }

    [HttpPost("load")]
    public ActionResult<string> Load([FromForm]UploadModel image)
    {
        byte[] data;
        using (var stream = image.Image.OpenReadStream())
        {
            if (!ImageUtils.TryToGetJpgImage(stream, out data))
            {
                var errors = new ExecutionResult();
                errors.AddError("Invalid image");
                return BadRequest(errors);
            }
        }

        return this.ResolveResult(m_ImageService.Load(data));
    }

    [HttpPost("crop/{imageKey}")]
    public async Task<ActionResult<string>> Crop(string imageKey, [FromBody]CropOptions options)
    {
        return this.ResolveResult(await m_ImageService.Crop(imageKey, options));
    }

    [HttpGet("{key}")]
    [DisableRequestSizeLimit]
    public async Task<ActionResult<byte[]>> GetImage(string key)
    {
        return this.ResolveResult(await m_ImageService.GetImage(key));
    }
}