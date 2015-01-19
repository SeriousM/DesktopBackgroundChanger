using System;
using DesktopBackgroundChanger.Sources.Wallhaven;

namespace DesktopBackgroundChanger.ViewModels
{
	public class BackgroundImageViewModel
	{
		private readonly WallhavenImageInformation _imageInformation;

		public BackgroundImageViewModel(WallhavenImageInformation imageInformation)
		{
			_imageInformation = imageInformation;
		}

		public bool ImageLoaded
		{
			get { return _imageInformation.ImageLoaded; }
			set { _imageInformation.ImageLoaded = value; }
		}

		public DateTime AddedTime
		{
			get { return _imageInformation.AddedTime; }
			set { _imageInformation.AddedTime = value; }
		}

		public string FavoritesCount
		{
			get { return _imageInformation.FavoritesCount; }
			set { _imageInformation.FavoritesCount = value; }
		}

		public string ViewsCount
		{
			get { return _imageInformation.ViewsCount; }
			set { _imageInformation.ViewsCount = value; }
		}

		public CategoryType Category
		{
			get { return _imageInformation.Category; }
			set { _imageInformation.Category = value; }
		}

		public string Size
		{
			get { return _imageInformation.Size; }
			set { _imageInformation.Size = value; }
		}

		public string Resolution
		{
			get { return _imageInformation.Resolution; }
			set { _imageInformation.Resolution = value; }
		}

		public PurityType Purity
		{
			get { return _imageInformation.Purity; }
			set { _imageInformation.Purity = value; }
		}

		public string ThumbnailUrl
		{
			get { return _imageInformation.ThumbnailUrl; }
			set { _imageInformation.ThumbnailUrl = value; }
		}

		public string ImageUrl
		{
			get { return _imageInformation.ImageUrl; }
			set { _imageInformation.ImageUrl = value; }
		}

		public string InformationUrl
		{
			get { return _imageInformation.InformationUrl; }
			set { _imageInformation.InformationUrl = value; }
		}

		public string ImageType
		{
			get { return _imageInformation.ImageType; }
			set { _imageInformation.ImageType = value; }
		}
	}
}