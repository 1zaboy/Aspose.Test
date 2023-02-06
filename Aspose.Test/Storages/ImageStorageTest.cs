using Aspose.Core.Storages;

namespace Aspose.Test.Storages;

[TestFixture]
public class ImageStorageTest
{
    private IImageStorage m_Storage;
    [SetUp]
    public void Setup()
    {
        m_Storage = new ImageStorage();
    }

    [Test]
    public void ImageStorage_AddGet_Test()
    {
        var key = Guid.NewGuid().ToString();
        var baseArray = new byte[] { 255 };
        m_Storage.Set(key, new byte[] { 255 });
        var value = m_Storage.Get(key);
        
        CollectionAssert.AreEqual(baseArray, value);
    }
}