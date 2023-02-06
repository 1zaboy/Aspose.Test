using Aspose.Core.Utils;
using NUnit.Framework;

namespace Aspose.Test.Utils;

[TestFixture]
public class RangeUtilsTest
{
    [Test]
    public void OutOfRange_InRange()
    {
        var value = RangeUtils.OutOfRange(5, 0, 10);
        Assert.AreEqual(value, 5);
    }
    
    [Test]
    public void OutOfRange_OutRange_ToBig()
    {
        var value = RangeUtils.OutOfRange(50, 0, 10);
        Assert.AreEqual(value, 10);
    }

    [Test] public void OutOfRange_OutRange_ToSmall()
    {
        var value = RangeUtils.OutOfRange(-50, 0, 10);
        Assert.AreEqual(value, 0);
    }
}