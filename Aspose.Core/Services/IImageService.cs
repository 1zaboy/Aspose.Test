using Aspose.Core.Models;
using Aspose.Data.Models;

namespace Aspose.Core.Services;

public interface IImageService
{
    ExecutionResult<string> Load(byte[] data);
    Task<ExecutionResult<string>> Crop(string imageKey, CropOptions options);
    Task<ExecutionResult<byte[]>> GetImage(string key);
}