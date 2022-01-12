using BusinessLibrary.Models.Planning.Icon;
using System;
using System.Drawing;

namespace BusinessLibrary.Models.Planning.Extension
{
	public static class IterationPlanningExtension
	{
		public static bool ShouldHasIcon(this IterationFeatureItemPlanningModel model)
		{
			return model.Icon != Icon.IconType.None;
		}


		public static Image SetImage(this IconType type, int size)
		{
			string fname = string.Empty;
			switch (type)
			{
				case IconType.New:
					fname = $@"{System.IO.Directory.GetCurrentDirectory()}\..\..\..\BusinessLibrary\Resource\Icon\new.png";
					break;
				case IconType.Running:
					fname = $@"{System.IO.Directory.GetCurrentDirectory()}\..\..\..\BusinessLibrary\Resource\Icon\running.png";
					break;
				case IconType.InReview:
					fname = $@"{System.IO.Directory.GetCurrentDirectory()}\..\..\..\BusinessLibrary\Resource\Icon\inreview.png";
					break;
				case IconType.Blocked:
					fname = $@"{System.IO.Directory.GetCurrentDirectory()}\..\..\..\BusinessLibrary\Resource\Icon\blocked.png";
					break;
				default:
					break;
			}

			if (string.IsNullOrEmpty(fname)) return null;

			var bmp = Bitmap.FromFile(fname);
			var thumb = (Bitmap)bmp.GetThumbnailImage(size, size, null, IntPtr.Zero);
			thumb.MakeTransparent();

			var icon = System.Drawing.Icon.FromHandle(thumb.GetHicon());

			return icon.ToBitmap();
		}

	}
}
