namespace Aspose.Core.Storages;

public interface IImageStorage
{
    string Set(string key, byte[] data);
    byte[]? Get(string key);
}