namespace Aspose.Core.Storages;

public class ImageStorage : IImageStorage
{
    private Dictionary<string, byte[]> m_Storage = new();

    public string Set(string key, byte[] data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));

        return m_Storage.TryAdd(key, data) ? key : "";
    }

    public byte[]? Get(string key)
    {
        if (m_Storage.TryGetValue(key, out byte[] value))
        {
            return value;
        }

        return null;
    }
}