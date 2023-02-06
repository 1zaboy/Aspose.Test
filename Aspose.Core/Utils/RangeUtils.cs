namespace Aspose.Core.Utils;

public static class RangeUtils
{
    public static int OutOfRange(int point, int start, int end)
    {
        int res = point;
        
        if (point < start)
        {
            res = start;
        }

        if (point > end)
        {
            res = end;
        }

        return res;
    }
}