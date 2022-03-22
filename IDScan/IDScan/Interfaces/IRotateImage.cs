using System;
namespace IDScan.Interfaces
{
    public interface IRotateImage
    {
        byte[] Rotate(System.IO.Stream imageStream, string filePath);
    }
}
