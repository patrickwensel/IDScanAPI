using System;

using IDScan.Droid.Services;
using Android.Graphics;
using IDScan.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(ImageInfo))]
namespace IDScan.Droid.Services
{
    public class ImageInfo : IImageInfo
    {
		public Tuple<int, int> GetFileWidthAndHeight(string file)
		{

			BitmapFactory.Options options = new BitmapFactory.Options();
			options.InJustDecodeBounds = true;

			BitmapFactory.DecodeFile(file, options);

			int width = options.OutWidth;
			int height = options.OutHeight;

			return new Tuple<int, int>(width, height);
		}
	}
}
