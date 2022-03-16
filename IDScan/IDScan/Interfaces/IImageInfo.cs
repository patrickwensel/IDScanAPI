using System;
namespace IDScan.Interfaces
{
    public interface IImageInfo
    {
        Tuple<int, int> GetFileWidthAndHeight(string file);
    }
}
