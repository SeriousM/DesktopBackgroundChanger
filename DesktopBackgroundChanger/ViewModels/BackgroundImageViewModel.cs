using DesktopBackgroundChanger.Sources.Wallhaven;

namespace DesktopBackgroundChanger.ViewModels
{
	public class BackgroundImageViewModel
	{
		private readonly WallhavenImageInformation _wallhavenImageInformation;

		public BackgroundImageViewModel(WallhavenImageInformation wallhavenImageInformation)
		{
			_wallhavenImageInformation = wallhavenImageInformation;
		}

		public string ImageType
		{
			get { return _wallhavenImageInformation.ImageType; }
		}
	}
}