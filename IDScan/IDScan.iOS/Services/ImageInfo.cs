using System;
using Foundation;
using IDScan.Interfaces;
using IDScan.iOS.Services;
using ImageIO;

[assembly: Xamarin.Forms.Dependency(typeof(ImageInfo))]
namespace IDScan.iOS.Services
{
    
	public class ImageInfo : IImageInfo
	{


		public Tuple<int, int> GetFileWidthAndHeight(string file)
		{

			int width;
			int height;

			using (var src = CGImageSource.FromUrl(NSUrl.FromFilename(file)))
			{

				CGImageOptions options = new CGImageOptions() { ShouldCache = false };

				width = (int)src.GetProperties(0, options).PixelWidth;
				height = (int)src.GetProperties(0, options).PixelHeight;
			}

			return new Tuple<int, int>(width, height);
		}
	}
}
