using Aspose.Core.Cropper;
using Aspose.Core.Models;
using Aspose.Core.Storages;
using Aspose.Data.Models;

namespace Aspose.Core.Services;

public class ImageService : IImageService
{
    // TODO: Should be in file system
    private IImageStorage m_Storage;

    public ImageService(IImageStorage storage)
    {
        m_Storage = storage;
    }

    public async Task<ExecutionResult<string>> CropAndSave(byte[] data, CropOptions options)
    {
        var result = new ExecutionResult<string>();
        byte[] cropImage;

        try
        {
            var croper = new CropperHandler(data);
            cropImage = await croper.Crop(options);
        }
        catch (Exception e)
        {
            result.AddError(e.Message);
            return result;
        }

        result.Result = m_Storage.Set(Guid.NewGuid().ToString(), cropImage);

        return result;
    }

    public ExecutionResult<string> Load(byte[] data)
    {
        var result = new ExecutionResult<string>();
        var key = m_Storage.Set(Guid.NewGuid().ToString(), data);

        if (string.IsNullOrWhiteSpace(key))
        {
            result.AddError("Error save image");
        }
        else
        {
            result.Result = key;
        }

        return result;
    }

    public async Task<ExecutionResult<string>> Crop(string imageKey, CropOptions options)
    {
        var result = new ExecutionResult<string>();
        byte[] cropImage;
        var image = m_Storage.Get(imageKey);

        if (image == null)
        {
            result.AddError("Image not founded");
            return result;
        }
        
        try
        {
            var croper = new CropperHandler(image);
            cropImage = await croper.Crop(options);
        }
        catch (Exception e)
        {
            result.AddError(e.Message);
            return result;
        }

        result.Result = m_Storage.Set(Guid.NewGuid().ToString(), cropImage);

        return result;
    }

    public async Task<ExecutionResult<byte[]>> GetImage(string key)
    {
        var result = new ExecutionResult<byte[]>();
        var image = m_Storage.Get(key);
        if (image is byte[] value)
        {
            result.Result = value;
        }
        else
        {
            result.AddError("Image not found");
        }

        return result;
    }
}