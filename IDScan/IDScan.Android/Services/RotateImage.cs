using System;
using System.IO;
using Android.Graphics;
using Android.Media;
using IDScan.Droid.Services;
using IDScan.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(RotateImage))]
namespace IDScan.Droid.Services
{
    public class RotateImage : IRotateImage
    {
        public int GetImageRotation(string filePath)
        {
            try
            {
                ExifInterface ei = new ExifInterface(filePath);
                Orientation orientation = (Orientation)ei.GetAttributeInt(ExifInterface.TagOrientation, (int)Orientation.Undefined);
                switch (orientation)
                {
                    case Orientation.Rotate90:
                        return 90;
                    case Orientation.Rotate180:
                        return 180;
                    case Orientation.Rotate270:
                        return 270;
                    case Orientation.Normal:
                        return 0;
                    case Orientation.Undefined:
                        return 270;
                    default:
                        return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public byte[] Rotate(System.IO.Stream imageStream, string filePath)
        {
            int rotationDegrees = GetImageRotation(filePath);
            byte[] byteArray = new byte[imageStream.Length];
            try
            {
                imageStream.Read(byteArray, 0, (int)imageStream.Length);

                Bitmap originalImage = BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length);
                Matrix matrix = new Matrix();
                matrix.PostRotate((float)rotationDegrees);

                Bitmap rotatedBitmap = Bitmap.CreateBitmap(originalImage, 0, 0, originalImage.Width,
                    originalImage.Height, matrix, true);

                using (MemoryStream ms = new MemoryStream())
                {
                    rotatedBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                return byteArray;
            }
        }
    }
}
